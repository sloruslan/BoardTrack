using Application.DTO.Board;
using Application.Extensions;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.DTO.Database.Models;
using Domain.Exceptions;
using Domain.Extensions;
using LinqToDB;
using LinqToDB.Data;
using Persistence.Database.DbContextFactory;

namespace Persistence.Database.Repository
{
    public class BoardRepository : BaseRepository, IBoardRepository
    {
        private readonly IMapper _mapper;
        public BoardRepository(IDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory)
        {
            _mapper = mapper;
        }

        public async Task<Board> CreateAsync(DataConnection db, Board request)
        {
            request.Id = await db.InsertWithInt64IdentityAsync(request);

            return request;
        }

        public async Task<(int TotalCount, List<Board> Payload)> GetAsync(FilteringBoardRequest filteringModel)
        {
            using var db = _dbContextFactory.Create();

            var query = db.Board
                               .WhereIfParameterNotNull(filteringModel.Id, x => x.Id == filteringModel.Id)
                               .WhereIfParameterNotNull(filteringModel.CreatedAt, x => x.CreatedAt == filteringModel.CreatedAt)
                               .WhereIfParameterNotNull(filteringModel.UpdatedAt, x => x.UpdatedAt == filteringModel.UpdatedAt)
                               .WhereIfParameterNotNull(filteringModel.Name, x => x.Name == filteringModel.Name)
                               .WhereIfParameterNotNull(filteringModel.Serial, x => x.Serial == filteringModel.Serial)
                               .WhereIfParameterNotNull(filteringModel.TypeId, x => x.TypeId == filteringModel.TypeId)
                               .WhereIfParameterNotNull(filteringModel.CurrentStepId, x => x.CurrentStepId == filteringModel.CurrentStepId);



            if (filteringModel.Sort.CheckParameters())
            {
                query = query.OrderBy(filteringModel.Sort!.OrderBy!, filteringModel.Sort.Ordering);
            }
            var totalCount = query.Count();
            var entities = await query.ToPaginatedListAsync(filteringModel.Limit, filteringModel.Offset);


            return (totalCount, entities);
        }

        public DataConnection GetDataConnection()
        {
            return _dbContextFactory.Create();
        }

        public async Task<Board> GetOneAsync(long id)
        {
            using var db = _dbContextFactory.Create();
            var result = await db.Board
                .FirstOrDefaultAsync(x =>
            x.Id == id);

            if (result == null)
            {
                throw new EntityNotFindException("Запись не найдена");
            }

            return result;
        }

        public async Task<Board> SetNextStepAsync(DataConnection db, long id, short nextStepId)
        {
            var entity = await ((DatabaseContext)db).Board
                .FirstOrDefaultAsync(x =>
            x.Id == id);

            if (entity == null)
            {
                throw new EntityNotFindException("Запись не найдена");
            }

            entity.UpdatedAt = DateTime.UtcNow;
            entity.CurrentStepId = nextStepId;

            await db.UpdateAsync(entity);

            return entity;
        }
    }
}
