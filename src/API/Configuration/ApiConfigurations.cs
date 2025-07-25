﻿using API.Service;
using Application.Configuration;
using Asp.Versioning;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;
using FusionCacheOptions =Application.Configuration.FusionCacheOptions;

namespace API.Configuration
{
    public static class ApiConfigurations
    {
        public static WebApplicationBuilder ConfigureAPI(this WebApplicationBuilder builder)
        {
            // Token validation

            builder.Services.AddCors();
            builder.Services.AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
                    opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
                    opt.JsonSerializerOptions.WriteIndented = true;
                    opt.JsonSerializerOptions.Encoder = JavaScriptEncoder.Default;
                    opt.JsonSerializerOptions.AllowTrailingCommas = false;
                    opt.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersioning(options =>
                {
                    options.ReportApiVersions = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                });

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {

                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                });


            builder.Services.AddScoped<JwtService>();

            return builder;
        }

        public static IServiceCollection AddTokenValidation(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Board Track", Version = "v1" });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Description = "Введите JWT токен в формате: Bearer {токен}"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            return services;
        }

        public static IServiceCollection AddFusionCache(this IServiceCollection services, IConfiguration configuration)
        {
            var fusionCacheOptions = configuration
                .GetSection(FusionCacheOptions.Section)
                .Get<FusionCacheOptions>()!;

            services.AddMemoryCache()
                .AddFusionCache()
                .WithDefaultEntryOptions(new FusionCacheEntryOptions(TimeSpan.FromMinutes(fusionCacheOptions.DurationMinutes)))
                .WithSerializer(new FusionCacheSystemTextJsonSerializer());

            return services;
        }
    }
}