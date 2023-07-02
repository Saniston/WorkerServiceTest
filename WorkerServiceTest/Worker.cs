namespace WorkerServiceTest
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;

        public Worker(
        IHostApplicationLifetime hostApplicationLifetime, ILogger<Worker> logger) =>
        (_hostApplicationLifetime, _logger) = (hostApplicationLifetime, logger);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"Iniciando serviço {DateTimeOffset.Now:G}");
            if (!Directory.Exists("wwwroot"))
            {
                var assembly = typeof(Program).Assembly.GetFiles();
            }
            await WebApplicationTest.Program.CreateHostBuilder(new string[] { "94" }).Build().RunAsync();
            _logger.LogInformation($"Serviço finalizado {DateTimeOffset.Now:G}");
            _hostApplicationLifetime.StopApplication();
        }
    }
}