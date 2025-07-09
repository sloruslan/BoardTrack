using Application.DTO.Board;
using Application.DTO.BoardHistory;
using Application.Extensions;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.DTO.Database.Models;
using Domain.Exceptions;
using Domain.Extensions;
using LinqToDB;
using LinqToDB.Data;
using Persistence.Database.DbContextFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database.Repository
{
    public class BoardHistoryRepository : BaseRepository, IBoardHistoryRepository
    {
        private readonly IMapper _mapper;
        public BoardHistoryRepository(IDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory)
        {
            _mapper = mapper;
        }

        public Task CreateAsync(DataConnection db, BoardHistory request)
        {
            return db.InsertAsync(request);
        }

        public async Task<(int TotalCount, List<BoardHistory> Payload)> GetAsync(FilteringBoardHistoryRequest filteringModel)
        {
            using var db = _dbContextFactory.Create();

            var query = db.BoardHistory
                               .WhereIfParameterNotNull(filteringModel.Id, x => x.Id == filteringModel.Id)
                               .WhereIfParameterNotNull(filteringModel.BoardId, x => x.BoardId == filteringModel.BoardId)
                               .WhereIfParameterNotNull(filteringModel.MovedAt, x => x.MovedAt == filteringModel.MovedAt)
                               .WhereIfParameterNotNull(filteringModel.FromStepId, x => x.FromStepId == filteringModel.FromStepId)
                               .WhereIfParameterNotNull(filteringModel.ToStepId, x => x.ToStepId == filteringModel.ToStepId)
                               .WhereIfParameterNotNull(filteringModel.MovedByUserId, x => x.MovedByUserId == filteringModel.MovedByUserId)
                               .WhereIfParameterNotNull(filteringModel.Comment, x => x.Comment == filteringModel.Comment);



            if (filteringModel.Sort.CheckParameters())
            {
                query = query.OrderBy(filteringModel.Sort!.OrderBy!, filteringModel.Sort.Ordering);
            }
            var totalCount = query.Count();
            var entities = await query.ToPaginatedListAsync(filteringModel.Limit, filteringModel.Offset);



            return (totalCount, entities);
        }

        public async Task<BoardHistory> GetOneAsync(long id)
        {
            using var db = _dbContextFactory.Create();
            var result = await db.BoardHistory.FirstOrDefaultAsync(x =>
            x.Id == id);

            if (result == null)
            {
                throw new EntityNotFindException("Запись не найдена");
            }

            return result;
        }
    }
}
