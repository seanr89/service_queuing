using RabbitMQ.Client;
using System.Text;
using Spectre.Console;
using Newtonsoft.Json;

AnsiConsole.Write(
    new FigletText("Rabbit Sender Auto")
        .LeftJustified()
        .Color(Color.Red));
try{
    var factory = new ConnectionFactory { HostName = "localhost" };
    var connection = factory.CreateConnection();
    var channel = connection.CreateModel();

    channel.QueueDeclare(queue: "emailer",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
    
    var count = AnsiConsole.Ask<int>("Enter a [blue]count[/]?");

    for(int i = 0; i <= count; i++)
    {
        AnsiConsole.MarkupLine($"current : [green]{i}[/] increment");
        var rec = EmailContentCreator.CreateBogusEmailContent();
        var content = JsonConvert.SerializeObject(rec);
        var body = Encoding.UTF8.GetBytes(content);

        channel.BasicPublish(exchange: string.Empty,
                        routingKey: "emailer",
                        basicProperties: null,
                        body: body);
        Thread.Sleep(250);
    }
}
catch
{
    Console.WriteLine($"Error");
    Environment.Exit(1);
}

