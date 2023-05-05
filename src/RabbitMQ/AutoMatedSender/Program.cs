using RabbitMQ.Client;
using System.Text;
using Spectre.Console;

AnsiConsole.Write(
    new FigletText("Rabbit Sender Auto")
        .LeftJustified()
        .Color(Color.Red));
try{
    // var factory = new ConnectionFactory { HostName = "localhost" };
    // var connection = factory.CreateConnection();
    // var channel = connection.CreateModel();

    // channel.QueueDeclare(queue: "emailer",
    //                     durable: false,
    //                     exclusive: false,
    //                     autoDelete: false,
    //                     arguments: null);
    
    var count = AnsiConsole.Ask<int>("Enter a [blue]count[/]?");

    for(int i = 0; i <= count; i++)
    {
        AnsiConsole.MarkupLine($"current : [green]{i}[/] increment");
        var rec = EmailContentCreator.CreateBogusEmailContent();
        Thread.Sleep(150);
    }
}
catch
{
    Console.WriteLine($"Error");
    Environment.Exit(1);
}

