using System.Text;
using Newtonsoft.Json;
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
    // channel.QueueDeclare(queue: "messager",
    //                         durable: false,
    //                         exclusive: false,
    //                         autoDelete: false,
    //                         arguments: null);
    channel.QueueDeclare(queue: "emailer",
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
        var jsonContent = JsonConvert.DeserializeObject<EmailContent>(message);
        Console.WriteLine($" [x] Received {jsonContent?.ToString()}");
    };
    channel.BasicConsume(queue: "emailer",
                        autoAck: true,
                        consumer: consumer);
    Console.ReadLine();
}