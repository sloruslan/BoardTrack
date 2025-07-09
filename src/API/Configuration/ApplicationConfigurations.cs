using Application.Interfaces.API;
using Application.Services.API;

namespace API.Configurations
{
    public static class ApplicationConfigurations
    {
        public static WebApplicationBuilder AddApplication(this WebApplicationBuilder builder)
        {
            // all configurations for application library should be there

            builder.Services.AddScoped<IInfoService, InfoService>();
            builder.Services.AddScoped<IBoardTypeService, BoardTypeService>();
            builder.Services.AddScoped<IBoardService, BoardService>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();

            return builder;
        }
    }
}
