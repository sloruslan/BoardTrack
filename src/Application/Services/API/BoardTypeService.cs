using Application.DTO;
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
    public class BoardTypeService : IBoardTypeService
    {
        private readonly IMapper _mapper;
        private readonly IBoardTypeRepository _repository;

        public BoardTypeService(
            IBoardTypeRepository repository,
            IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<BoardTypeResponse> CreateAsync(CreateBoardTypeRequest request)
        {
            return _mapper.Map<BoardTypeResponse>(await _repository.CreateAsync(request));
        }

        public Task DeleteAsync(long id)
        {
            return _repository.DeleteAsync(id);
        }

        public async Task<List<BoardTypeResponse>> GetAsync(FilteringBoardTypeRequest filteringModel)
        {
            var res = await _repository.GetAsync(filteringModel);

            return _mapper.Map<List<BoardTypeResponse>>(res.Payload);
        }

        public async Task<BoardTypeResponse> GetOneAsync(long id)
        {
            return _mapper.Map<BoardTypeResponse>(await _repository.GetOneAsync(id));
        }

        public async Task<WithCountResponse<BoardTypeResponse>> GetWithCountAsync(FilteringBoardTypeRequest filteringModel)
        {
            var res = await _repository.GetAsync(filteringModel);

            return new WithCountResponse<BoardTypeResponse>()
            {
                TotalCount = res.TotalCount,
                Payload = _mapper.Map<List<BoardTypeResponse>>(res.Payload)
            };
        }

        public async Task<BoardTypeResponse> UpdateAsync(long id, UpdateBoardTypeRequest request)
        {
            return _mapper.Map<BoardTypeResponse>(await _repository.UpdateAsync(id, request));
        }
    }
}
