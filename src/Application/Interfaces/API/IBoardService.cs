using Application.DTO;
using Application.DTO.Board;

namespace Application.Interfaces.API
{
    public interface IBoardService
    {
        Task<BoardResponse> CreateAsync(CreateBoardRequest request);
        Task<BoardResponse> GetOneAsync(long id);
        Task<List<BoardResponse>> GetAsync(FilteringBoardRequest filteringModel);
        Task<WithCountResponse<BoardResponse>> GetWithCountAsync(FilteringBoardRequest filteringModel);
        Task<BoardResponse> MoveBoardAsync(MoveBoardRequest request);
        
    }
}
