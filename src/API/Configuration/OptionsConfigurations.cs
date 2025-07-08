using API.Configuration.Options;
using Microsoft.Extensions.Options;
using Persistence.Configurations;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Configurations
{
    public static class OptionsConfigurations
    {
        public static WebApplicationBuilder ConfigureOptions(this WebApplicationBuilder builder)
        {
            // register and configure IOptions there
            builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigureOptions>();
            builder.Services.Configure<DatabaseOptions>(builder.Configuration.GetSection(DatabaseOptions.Section));


            return builder;
        }
    }
}
