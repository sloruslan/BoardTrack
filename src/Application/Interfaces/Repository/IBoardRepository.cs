using Application.DTO.Board;
using Application.DTO.BoardType;
using Domain.DTO.Database.Models;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IBoardRepository
    {
        DataConnection GetDataConnection();
        Task<Board> CreateAsync(DataConnection db, Board request);
        Task<Board> GetOneAsync(long id);
        Task<(int TotalCount, List<Board> Payload)> GetAsync(FilteringBoardRequest filteringModel);
        Task<Board> SetNextStepAsync(DataConnection db, long id, short nextStepId);

    }
}
