using Application.DTO.Board;
using Application.DTO.BoardType;
using Domain.DTO.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IBoardRepository
    {
        Task<Board> CreateAsync(Board request);
        Task<Board> GetOneAsync(long id);
        Task<(int TotalCount, List<Board> Payload)> GetAsync(FilteringBoardRequest filteringModel);
        Task<Board> SetNextStepAsync(long id, short nextStepId);

    }
}
