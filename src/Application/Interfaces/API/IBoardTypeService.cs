using Application.DTO;
using Application.DTO.BoardType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.API
{
    public interface IBoardTypeService
    {
        Task<BoardTypeResponse> CreateAsync(CreateBoardTypeRequest request);
        Task DeleteAsync(long id);
        Task<BoardTypeResponse> GetOneAsync(long id);
        Task<List<BoardTypeResponse>> GetAsync(FilteringBoardTypeRequest filteringModel);
        Task<WithCountResponse<BoardTypeResponse>> GetWithCountAsync(FilteringBoardTypeRequest filteringModel);
        Task<BoardTypeResponse> UpdateAsync(long id, UpdateBoardTypeRequest request);
    }
}
