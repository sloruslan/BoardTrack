using Application.DTO.Board;
using Application.DTO.BoardHistory;
using Domain.DTO.Database.Models;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IBoardHistoryRepository
    {
        Task CreateAsync(DataConnection db, BoardHistory request);
        Task<BoardHistory> GetOneAsync(long id);
        Task<(int TotalCount, List<BoardHistory> Payload)> GetAsync(FilteringBoardHistoryRequest filteringModel);
    }
}
