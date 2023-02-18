using Microsoft.EntityFrameworkCore;
using Todo2.Models;

namespace Todo2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<TodoContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("TodoDatabase")));
            //automapper service inject 
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}