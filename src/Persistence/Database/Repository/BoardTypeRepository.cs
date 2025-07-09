using Application.DTO.BoardType;
using Application.Extensions;
using Application.Interfaces.Repository;
using AutoMapper;
using Domain.DTO.Database.Models;
using Domain.Exceptions;
using Domain.Extensions;
using LinqToDB;
using Persistence.Database.DbContextFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database.Repository
{
    public class BoardTypeRepository : BaseRepository, IBoardTypeRepository
    {
        private readonly IMapper _mapper;
        public BoardTypeRepository(IDbContextFactory dbContextFactory, IMapper mapper) : base(dbContextFactory)
        {
            _mapper = mapper;
        }

        public async Task<BoardType> CreateAsync(CreateBoardTypeRequest request)
        {
            using var db = _dbContextFactory.Create();

            var newEntity = _mapper.Map<BoardType>(request);
            newEntity.Id = await db.InsertWithInt64IdentityAsync(newEntity);

            return newEntity;
        }

        public async Task DeleteAsync(long id)
        {
            using var db = _dbContextFactory.Create();

            var entity = await db.BoardType.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFindException("Объект не найден!");
            }

           
            await db.DeleteAsync(entity);
        }

        public async Task<(int TotalCount, List<BoardType> Payload)> GetAsync(FilteringBoardTypeRequest filteringModel)
        {
            using var db = _dbContextFactory.Create();

            var query = db.BoardType
                               .WhereIfParameterNotNull(filteringModel.Id, x => x.Id == filteringModel.Id)
                               .WhereIfParameterNotNull(filteringModel.CreatedAt, x => x.CreatedAt == filteringModel.CreatedAt)
                               .WhereIfParameterNotNull(filteringModel.UpdatedAt, x => x.UpdatedAt == filteringModel.UpdatedAt)
                               .WhereIfParameterNotNull(filteringModel.Name, x => x.Name == filteringModel.Name)
                               .WhereIfParameterNotNull(filteringModel.Description, x => x.Description == filteringModel.Description);

           

            if (filteringModel.Sort.CheckParameters())
            {
                query = query.OrderBy(filteringModel.Sort!.OrderBy!, filteringModel.Sort.Ordering);
            }
            var totalCount = query.Count();
            var entities = await query.ToPaginatedListAsync(filteringModel.Limit, filteringModel.Offset);
            


            return (totalCount, entities);
        }

        public async Task<BoardType> GetOneAsync(long id)
        {
            using var db = _dbContextFactory.Create();
            var result = await db.BoardType.FirstOrDefaultAsync(x =>
            x.Id == id);

            if (result == null)
            {
                throw new EntityNotFindException("Запись не найдена");
            }

            return result;
        }

        public async Task<BoardType> UpdateAsync(long id, UpdateBoardTypeRequest request)
        {
            using var context = _dbContextFactory.Create();

            var entity = await context.BoardType.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                throw new EntityNotFindException("Запись не найдена");
            }

            _mapper.Map(request, entity);
            entity.UpdatedAt = DateTime.UtcNow;

            await context.UpdateAsync(entity);

            return entity;
        }
    }
}
