using Application.DTO.BoardType;
using Domain.DTO.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IBoardTypeRepository
    {
        Task<BoardType> CreateAsync(CreateBoardTypeRequest request);
        Task DeleteAsync(long id);
        Task<BoardType> GetOneAsync(long id);
        Task<(int TotalCount, List<BoardType> Payload)> GetAsync(FilteringBoardTypeRequest filteringModel);
        Task<BoardType> UpdateAsync(long id, UpdateBoardTypeRequest request);
    }
}
