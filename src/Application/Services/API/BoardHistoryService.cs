using Application.DTO;
using Application.DTO.Board;
using Application.DTO.BoardHistory;
using Application.DTO.BoardType;
using Application.Interfaces.API;
using Application.Interfaces.Repository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.API
{
    public class BoardHistoryService : IBoardHistoryService
    {
        private readonly IMapper _mapper;
        private readonly IBoardHistoryRepository _repository;

        public BoardHistoryService(
            IBoardHistoryRepository repository,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<List<BoardHistoryResponse>> GetAsync(FilteringBoardHistoryRequest filteringModel)
        {
            var res = await _repository.GetAsync(filteringModel);

            return _mapper.Map<List<BoardHistoryResponse>>(res.Payload);
        }

        public async Task<BoardHistoryResponse> GetOneAsync(long id)
        {
            return _mapper.Map<BoardHistoryResponse>(await _repository.GetOneAsync(id));
        }

        public async Task<WithCountResponse<BoardHistoryResponse>> GetWithCountAsync(FilteringBoardHistoryRequest filteringModel)
        {
            var res = await _repository.GetAsync(filteringModel);

            return new WithCountResponse<BoardHistoryResponse>()
            {
                TotalCount = res.TotalCount,
                Payload = _mapper.Map<List<BoardHistoryResponse>>(res.Payload)
            };
        }
    }
}
