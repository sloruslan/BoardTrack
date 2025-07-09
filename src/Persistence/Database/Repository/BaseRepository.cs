using Persistence.Database.DbContextFactory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Database.Repository
{
    public abstract class BaseRepository
    {
        protected readonly IDbContextFactory _dbContextFactory;

        protected BaseRepository(IDbContextFactory contextFactory)
        {
            _dbContextFactory = contextFactory;
        }
    }
}
