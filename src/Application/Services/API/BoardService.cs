using Application.DTO;
using Application.DTO.Board;
using Application.DTO.BoardType;
using Application.Extensions;
using Application.Interfaces.API;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.DTO.Database.Models;
using Domain.Enums;
using Domain.Exceptions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.API
{
    public class BoardService : IBoardService
    {
        private readonly ILogger<BoardService> _logger;
        private readonly IMapper _mapper;
        private readonly IBoardRepository _boardRepository;
        private readonly IProductionStepRuleRepository _productionStepRuleRepository;
        private readonly IBoardHistoryRepository _boardHistoryRepository;

        public BoardService(
            ILogger<BoardService> logger,
            IBoardRepository boardRepository,
            IMapper mapper,
            IProductionStepRuleRepository productionStepRuleRepository,
            IBoardHistoryRepository boardHistoryRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _boardRepository = boardRepository;
            _productionStepRuleRepository = productionStepRuleRepository;
            _boardHistoryRepository = boardHistoryRepository;
        }

        public async Task<BoardResponse> CreateAsync(CreateBoardRequest request, long userId)
        {
            var entity = _mapper.Map<Board>(request);
            entity.CurrentStepId = (short)ProductionStepEnum.Registration;

            using (var db = _boardRepository.GetDataConnection())
            {
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var res = await _boardRepository.CreateAsync(db, entity);
                        await _boardHistoryRepository.CreateAsync(db, new BoardHistory()
                        {
                            BoardId = res.Id,
                            MovedAt = DateTime.UtcNow,
                            MovedByUserId = userId,
                            FromStepId = null,
                            ToStepId = (short)ProductionStepEnum.Registration,
                            Comment = "Регистрация новой платы"
                        });

                        transaction.Commit();
                        return _mapper.Map<BoardResponse>(res);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error Move Board");
                        transaction.Rollback();
                        throw new BoardMoveException(ex.ToString());
                    }
                }
            }
            
        }

        public async Task<List<BoardResponse>> GetAsync(FilteringBoardRequest filteringModel)
        {
            var res = await _boardRepository.GetAsync(filteringModel);

            return _mapper.Map<List<BoardResponse>>(res.Payload);
        }

        public async Task<BoardResponse> GetOneAsync(long id)
        {
            return _mapper.Map<BoardResponse>(await _boardRepository.GetOneAsync(id));
        }

        public async Task<WithCountResponse<BoardResponse>> GetWithCountAsync(FilteringBoardRequest filteringModel)
        {
            var res = await _boardRepository.GetAsync(filteringModel);

            return new WithCountResponse<BoardResponse>()
            {
                TotalCount = res.TotalCount,
                Payload = _mapper.Map<List<BoardResponse>>(res.Payload)
            };
        }

        public async Task<BoardResponse> MoveBoardAsync(MoveBoardRequest request, long userId)
        {
            var board = await _boardRepository.GetOneAsync(request.BoardId);

            var validSteps = _productionStepRuleRepository.GetValidSteps(board.CurrentStepId);
            if (!validSteps.Contains(request.NextStepId))
            {
                throw new StepNotAllowedException
                    ($"Переход с шага '{((ProductionStepEnum)board.CurrentStepId).ToRussianName()}' на шаг '{((ProductionStepEnum)request.NextStepId).ToRussianName()}' не разрешен");
            }

            var res = await MoveBoard(board, request, userId);
            return _mapper.Map<BoardResponse>(res);

        }

        private async Task<Board> MoveBoard(Board board, MoveBoardRequest request, long userId)
        {
            using (var db = _boardRepository.GetDataConnection())
            {
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        var res = await _boardRepository.SetNextStepAsync(db, request.BoardId, request.NextStepId);
                        await _boardHistoryRepository.CreateAsync(db, new BoardHistory()
                        {
                            BoardId = board.Id,
                            MovedAt = DateTime.UtcNow,
                            MovedByUserId = userId,
                            FromStepId = board.CurrentStepId,
                            ToStepId = request.NextStepId
                        });
                        
                        transaction.Commit();
                        return res;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error Move Board");
                        transaction.Rollback();
                        throw new BoardMoveException(ex.ToString());
                    }
                }
            }    
                

            
        }
    }
}
