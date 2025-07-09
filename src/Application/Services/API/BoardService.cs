using Application.DTO;
using Application.DTO.Board;
using Application.DTO.BoardType;
using Application.Interfaces.API;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.DTO.Database.Models;
using Domain.Enums;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.API
{
    public class BoardService : IBoardService
    {
        private readonly IMapper _mapper;
        private readonly IBoardRepository _repository;
        private readonly IProductionStepRuleRepository _productionStepRuleRepository;

        public BoardService(
            IBoardRepository repository,
            IMapper mapper,
            IProductionStepRuleRepository productionStepRuleRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _productionStepRuleRepository = productionStepRuleRepository;
        }

        public async Task<BoardResponse> CreateAsync(CreateBoardRequest request)
        {
            var entity = _mapper.Map<Board>(request);
            entity.CurrentStepId = (short)ProductionStepEnum.Registration;

            return _mapper.Map<BoardResponse>(await _repository.CreateAsync(entity));
        }

        public async Task<List<BoardResponse>> GetAsync(FilteringBoardRequest filteringModel)
        {
            var res = await _repository.GetAsync(filteringModel);

            return _mapper.Map<List<BoardResponse>>(res.Payload);
        }

        public async Task<BoardResponse> GetOneAsync(long id)
        {
            return _mapper.Map<BoardResponse>(await _repository.GetOneAsync(id));
        }

        public async Task<WithCountResponse<BoardResponse>> GetWithCountAsync(FilteringBoardRequest filteringModel)
        {
            var res = await _repository.GetAsync(filteringModel);

            return new WithCountResponse<BoardResponse>()
            {
                TotalCount = res.TotalCount,
                Payload = _mapper.Map<List<BoardResponse>>(res.Payload)
            };
        }

        public async Task<BoardResponse> MoveBoardAsync(MoveBoardRequest request)
        {
            var board = await _repository.GetOneAsync(request.BoardId);

            var validSteps = _productionStepRuleRepository.GetValidSteps(board.CurrentStepId);
            if (!validSteps.Contains(request.NextStepId))
            {
                throw new StepNotAllowedException($"Переход с шага {board.CurrentStepId} на шаг {request.NextStepId} не разрешен");
            }

            var res = await _repository.SetNextStepAsync(request.BoardId, request.NextStepId);
            return _mapper.Map<BoardResponse>(res);

        }
    }
}
