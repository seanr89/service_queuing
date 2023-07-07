
using System.Text;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

public class ReceiverService : IHostedService, IDisposable
{
    private readonly ILogger _logger;
    private Timer _timer;
    internal readonly string _host;
    internal int count = 0;

    public ReceiverService(ILogger<ReceiverService> logger)
    {
        _host = Environment.GetEnvironmentVariable("host") ?? "localhost";

        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Service is starting.");
        
        var factory = new ConnectionFactory { HostName = _host };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();
        var channelMessager = connection.CreateModel();

        channelMessager.QueueDeclare(queue: "messager",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
        
        var consumerMessager = new EventingBasicConsumer(channelMessager);
        consumerMessager.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            //var jsonContent = JsonConvert.DeserializeObject<EmailContent>(message);
            Console.WriteLine($" [x] Messager: Received {message}");
            count++;
        };

        channelMessager.BasicConsume(queue: "messager",
            autoAck: true,
            consumer: consumerMessager);

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(3));

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        _logger.LogInformation("Service is running.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}