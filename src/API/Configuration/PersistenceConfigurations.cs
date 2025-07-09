using Application.Interfaces;
using Application.Interfaces.API;
using Application.Interfaces.Repository;
using Persistence.Database.DbContextFactory;
using Persistence.Database.Repository;
using Persistence.Repository;
using Persistence.Service;

namespace API.Configuration
{
    public static class PersistenceConfigurations
    {
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            // all configurations for persistence library should be there

            builder.Services.AddSingleton<IDatabaseMaintenanceService, DatabaseMaintenanceService>();

            builder.Services.AddSingleton<IDbContextFactory, DbContextFactory>();
            builder.Services.AddTransient<IBoardTypeRepository, BoardTypeRepository>();
            builder.Services.AddTransient<IBoardRepository, BoardRepository>();
            builder.Services.AddTransient<IProductionStepRuleRepository, ProductionStepRuleRepository>();
            builder.Services.AddTransient<IBoardHistoryRepository, BoardHistoryRepository>();
            builder.Services.AddTransient<IUserRepository, UserRepository>();

            return builder;
        }
    }
}
