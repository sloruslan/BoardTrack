namespace API.Configurations
{
    public static class PersistenceConfigurations
    {
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            // all configurations for persistence library should be there
            //builder.Services.AddDbContext<DataBaseContext>(options =>
            //    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            //builder.Services.AddSingleton<IDbContextFactory, DataBaseContextFactory>();
            //builder.Services.AddTransient<IPowerUnitRepository, PowerUnitRepository>();
            //builder.Services.AddTransient<IStationRepository, StationRepository>();
            //builder.Services.AddTransient<IUserRepository, UserRepository>();

            return builder;
        }
    }
}
