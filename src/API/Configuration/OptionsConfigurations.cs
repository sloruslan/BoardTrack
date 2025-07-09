using Application.Configuration;
using Persistence.Configurations;

namespace API.Configuration
{
    public static class OptionsConfigurations
    {
        public static WebApplicationBuilder ConfigureOptions(this WebApplicationBuilder builder)
        {
            // register and configure IOptions there
            builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.Section));
            builder.Services.Configure<PartitionOptions>(builder.Configuration.GetSection(nameof(PartitionOptions)));


            return builder;
        }
    }
}
