using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Spectre.Console;

AnsiConsole.Write(
    new FigletText("Rabbit Receiver")
        .LeftJustified()
        .Color(Color.Red));

var factory = new ConnectionFactory { HostName = "localhost" };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();

while(true){
    channel.QueueDeclare(queue: "messager",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);

    Console.WriteLine(" [*] Waiting for messages.");

    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [x] Received {message}");
    };
    channel.BasicConsume(queue: "messager",
                        autoAck: true,
                        consumer: consumer);
    Console.ReadLine();
}