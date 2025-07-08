using Persistence.AutoMapper.Profiles;
using System.Reflection;

namespace API.Configurations
{
    public static class AutoMapperConfigurations
    {
        public static WebApplicationBuilder AddAutoMapper(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(MappingProfile)));

            return applicationBuilder;
        }
    }
}
