using Serilog;

namespace API.Configurations
{
    public static class LoggerConfigurations
    {
        public static WebApplicationBuilder ConfigureLogger(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Host.UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(applicationBuilder.Configuration);
            });

            return applicationBuilder;
        }
    }
}
