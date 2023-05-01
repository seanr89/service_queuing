// See https://aka.ms/new-console-template for more information
using Spectre.Console;
using RabbitMQ.Client;
using System.Text;

var factory = new ConnectionFactory { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

channel.QueueDeclare(queue: "messager",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

AnsiConsole.Write(
    new FigletText("Rabbit Sender")
        .LeftJustified()
        .Color(Color.Red));
        
while(true){
    //Console.WriteLine("Hello, World!");

    SendMessage();
}

Environment.Exit(1);


/// <summary>
/// Simple message test
/// </summary>
void SendMessage()
{
    var message = AnsiConsole.Ask<string>("What's your [green]message[/]?");
    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange: string.Empty,
                     routingKey: "messager",
                     basicProperties: null,
                     body: body);
    Console.WriteLine($" [x] Sent {message}");

}