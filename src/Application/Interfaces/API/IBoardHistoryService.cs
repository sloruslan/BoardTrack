using Application.DTO.Board;
using Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO.BoardHistory;

namespace Application.Interfaces.API
{
    public interface IBoardHistoryService
    {
        Task<BoardHistoryResponse> GetOneAsync(long id);
        Task<List<BoardHistoryResponse>> GetAsync(FilteringBoardHistoryRequest filteringModel);
        Task<WithCountResponse<BoardHistoryResponse>> GetWithCountAsync(FilteringBoardHistoryRequest filteringModel);
    }
}
