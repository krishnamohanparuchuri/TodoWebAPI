using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Data;
using TodoWebAPI.HelperMethods;
using TodoWebAPI.Repository;
using TodoWebAPI.Authorization;
using System.Text.Json.Serialization;

namespace TodoWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors();
            builder.Services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            // IOC containers
            builder.Services.AddScoped<ITodoRepository, TodoRepository>();
            builder.Services.AddScoped<IPassword, Password>();
            builder.Services.AddScoped<IJsonwebToken, JsonWebtoken>();
            builder.Services.AddScoped<IJwtUtils, JwtUtils>();
            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtSettings"));
            builder.Services.AddDbContext<TodoApiContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}