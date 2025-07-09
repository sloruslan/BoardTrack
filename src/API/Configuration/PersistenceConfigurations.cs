using Application.Interfaces.Repository;
using Persistence.Database.DbContextFactory;
using Persistence.Database.Repository;

namespace API.Configuration
{
    public static class PersistenceConfigurations
    {
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            // all configurations for persistence library should be there


            builder.Services.AddSingleton<IDbContextFactory, DbContextFactory>();
            builder.Services.AddTransient<IBoardTypeRepository, BoardTypeRepository>();
            builder.Services.AddTransient<IBoardRepository, BoardRepository>();
            builder.Services.AddTransient<IProductionStepRuleRepository, ProductionStepRuleRepository>();
            builder.Services.AddTransient<IBoardHistoryRepository, BoardHistoryRepository>();

            return builder;
        }
    }
}
