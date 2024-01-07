
using Draw.API.DbContexts;
using Draw.API.Repositories;
using Draw.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Draw.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DrawDbContext>(
                opt => opt.UseSqlite(builder.Configuration["ConnectionStrings:DrawDbConnectionStrings"])
                );

            builder.Services.AddScoped<IDrawRepository, DrawRepository>();
            builder.Services.AddScoped<IBusinessService, BusinessService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            // message for API is running for the main route path
            app.MapGet("/", () => "Group Draw API is up!");

            app.Run();
        }
    }
}
