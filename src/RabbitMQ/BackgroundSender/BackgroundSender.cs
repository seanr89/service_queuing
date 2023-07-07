
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Spectre.Console;
using Newtonsoft.Json;
using System.Text;

public class BackgroundSender : BackgroundService
{
    private readonly string _name;
    private readonly ILogger<BackgroundSender> _logger;
    internal readonly string _host;

    public BackgroundSender(ILogger<BackgroundSender> logger)
    {
        _logger = logger;
        _name = Util.CreateRandomName();
        _host = Environment.GetEnvironmentVariable("host") ?? "localhost";
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            await BackgroundProcessing(stoppingToken);
            await Task.Delay(2500, stoppingToken);
        }
    }

    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Random r = new Random();
            int rInt = r.Next(0, 55);

            try
            {
                var factory = new ConnectionFactory { HostName = _host };
                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "emailer",
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

                for(int i = 0; i <= rInt; i++)
                {
                    //AnsiConsole.MarkupLine($"current : [green]{i}[/] increment");
                    var rec = EmailContentCreator.CreateBogusEmailContent();
                    rec.Sender = _name;
                    var content = JsonConvert.SerializeObject(rec);
                    var body = Encoding.UTF8.GetBytes(content);

                    channel.BasicPublish(exchange: string.Empty,
                                    routingKey: "emailer",
                                    basicProperties: null,
                                    body: body);
                    Thread.Sleep(125);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, 
                    "Error occurred executing");
            }
        }
    }

    public override async Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Queued Hosted Service is stopping.");

        await base.StopAsync(stoppingToken);
    }


}