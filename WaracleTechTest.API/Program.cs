using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using WaracleTechTest.Business.Interfaces;
using WaracleTechTest.Business.Repositories;
using WaracleTechTest.Migrations;

namespace WaracleTechTest.API
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connection = "Server=tcp:jcooper310793.database.windows.net,1433;Initial Catalog=WaracleContext;Uid=jcooper310793;Pwd=!tjlmathe10;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

            var services = builder.Services;

            services.AddDbContext<WaracleContext>((serviceProvider, optionsBuilder) =>
                optionsBuilder.UseSqlServer(connection, options =>
                {
                    options.CommandTimeout((int)TimeSpan.FromMinutes(30).TotalSeconds);
                    options.EnableRetryOnFailure();
                    options.MigrationsAssembly("WaracleTechTest.Migrations");
                }));

            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IDatabaseRepository, DatabaseRepository>();
            services.AddScoped<IHotelRepository, HotelRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();

            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "WaracleTechTest.API",
                    Version = "v1"
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.DisplayRequestDuration();
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WaracleTechTest.API v1");
                });
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }
    }
}