using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Teste_Pratico_API_TPC.Data;
using Teste_Pratico_API_TPC.Extensions;
using Teste_Pratico_API_TPC.Interfaces;
using Teste_Pratico_API_TPC.Repositories;
using Teste_Pratico_API_TPC.Services;
using Swashbuckle.AspNetCore.Swagger;
using Teste_Pratico_API_TPC.Responses;

namespace Teste_Pratico_API_TPC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BancoTPCContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // AdicionarAsync services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //builder.Services.AddSwaggerGen();

            //Swagger
            builder.Services.AdicionarConfiguracaoSwagger();

            //JWT
            builder.Services.AdicionarConfiguracaoJwtEF(builder.Configuration);

            //Usuario
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<UsuarioService>();

            //Tarefas
            builder.Services.AddScoped<ITarefaRepository, TarefaRepository>();
            builder.Services.AddScoped<TarefaService>();

            //Autenticacao
            builder.Services.AddScoped<IAutenticacao, AutenticacaoService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}