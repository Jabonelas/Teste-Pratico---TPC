﻿using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Teste_Pratico_API_TPC.Data;
using Teste_Pratico_API_TPC.Interfaces;
using Teste_Pratico_API_TPC.Services;

namespace Teste_Pratico_API_TPC.Extensions
{
    public static class InjecaoDependenciaJWT
    {
        public static IServiceCollection AdicionarConfiguracaoJwtEF(this IServiceCollection service,
            IConfiguration configuration)
        {
            service.AddDbContext<BancoTPCContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(BancoTPCContext).Assembly.FullName));
            });

            service.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = configuration["jwt:issuer"],
                    ValidAudience = configuration["jwt:audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:secretKey"])),
                    ClockSkew = TimeSpan.Zero
                };

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.ContentType = "application/json";

                        await context.Response.WriteAsJsonAsync(new
                        {
                            message = "Acesso não autorizado. Token inválido ou expirado."
                        });
                    }
                };
            });

            service.AddScoped<IAutenticacao, AutenticacaoService>();

            return service;
        }
    }
}