
using Microsoft.EntityFrameworkCore;
using Stock_Buy.API.Data;
using Stock_Buy.API.ExceptionHandling;
using Stock_Buy.API.Services.Abstract;
using Stock_Buy.API.Services.Concrete;

namespace Stock_Buy.API
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

            builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DB_CONNECTIONSTRING"), configuration =>
                    {
                        configuration.UseHierarchyId();
                    }));

            builder.Services.AddScoped<IBundleService, BundleService>();
            builder.Services.AddScoped<IPartService, PartService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlingMiddleWare>();

            app.MapControllers();

            app.Run();
        }
    }
}
