using Application.Configuration;
using Application.Interfaces.API;
using Application.Jobs;
using Application.Services.API;
using Npgsql;
using Persistence.Configurations;
using Quartz;

namespace API.Configuration
{
    public static class ApplicationConfigurations
    {
        public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
        {
            // all configurations for application library should be there

            builder.Services.AddScoped<IInfoService, InfoService>();
            builder.Services.AddScoped<IBoardTypeService, BoardTypeService>();
            builder.Services.AddScoped<IBoardService, BoardService>();
            builder.Services.AddScoped<IBoardHistoryService, BoardHistoryService>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();

            return builder;
        }

        public static IServiceCollection AddSchedulerQuartz(this IServiceCollection services, IConfiguration configuration)
        {
            var cron = configuration.GetRequiredSection(nameof(CronJobsScheduleOptions))
             .GetValue<string>("DatabasePartitionJob")!;

            //var databaseOptions = new DatabaseOptions();
            //configuration.GetSection(DatabaseOptions.Section).Bind(databaseOptions);

            //var connStringBuilder = new NpgsqlConnectionStringBuilder
            //{
            //    Host = databaseOptions.Host,
            //    Port = databaseOptions.Port,
            //    Database = databaseOptions.Name,
            //    SearchPath = "validator",
            //    Username = databaseOptions.User,
            //    Password = databaseOptions.Password,
            //    CommandTimeout = databaseOptions.CommandTimeout,
            //    ConnectionIdleLifetime = databaseOptions.ConnectionIdLifetime,
            //    Pooling = databaseOptions.Pooling,
            //    KeepAlive = databaseOptions.KeepAlive,
            //    TcpKeepAlive = databaseOptions.TcpKeepAlive
            //};


            services.Configure<QuartzOptions>(options =>
            {
                options.SchedulerId = "AUTO";
            });

            services.AddQuartz(q =>
            {
                //q.UsePersistentStore(x =>
                //{
                //    x.UseProperties = true;
                //    x.UsePostgres(connStringBuilder.ToString());
                //    x.UseNewtonsoftJsonSerializer();
                //    x.UseClustering();
                //});

                q.AddJob<DatabasePartitionJob>(c => c.WithIdentity("database-partition-job"));
                q.AddTrigger(c => c.ForJob("database-partition-job")
                    .WithIdentity("database-partition-job-trigger")
                    .WithCronSchedule(cron));

                q.UseDefaultThreadPool();
            });
            services.AddQuartzHostedService(options =>
            {
                // when shutting down we want jobs to complete gracefully
                options.WaitForJobsToComplete = true;
            });

            return services;
        }
    }
}
