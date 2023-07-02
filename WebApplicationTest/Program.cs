namespace WebApplicationTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    var urls = new List<string>();
                    foreach (var port in args)
                    {
                        urls.Add($"http://localhost:{port}");
                        urls.Add($"http://{Environment.MachineName}:{port}");
                    }
                    webBuilder.UseUrls(urls.ToArray());

                }).ConfigureServices(c =>
                {
                });
    }
}