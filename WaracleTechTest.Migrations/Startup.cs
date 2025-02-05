using Microsoft.EntityFrameworkCore;

namespace WaracleTechTest.Migrations
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var connection = "Server=tcp:jcooper310793.database.windows.net,1433;Initial Catalog=WaracleContext;Uid=jcooper310793;Pwd=!tjlmathe10;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

            services.AddDbContext<WaracleContext>(opts => opts
                .UseSqlServer(connection, options => options.MigrationsAssembly("WaracleTechTest.Migrations")
                .EnableRetryOnFailure()));
        }

        public void Configure(IApplicationBuilder app)
        {
            var scopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();

            using var scope = scopeFactory.CreateScope();
            using var context = scope.ServiceProvider.GetService<WaracleContext>();

            if (context == null)
            {
                return;
            }

            context.Database.Migrate();
        }
    }
}