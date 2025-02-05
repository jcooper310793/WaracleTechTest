namespace WaracleTechTest.Migrations
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().StartAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseContentRoot(Directory.GetCurrentDirectory());
                    webBuilder.UseStartup<Startup>();
                });
    }
}