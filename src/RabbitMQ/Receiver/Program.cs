﻿using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Spectre.Console;

AnsiConsole.Write(
    new FigletText("Rabbit Receiver")
        .LeftJustified()
        .Color(Color.Red));

var host = Environment.GetEnvironmentVariable("host") ?? "localhost";

Console.WriteLine($"Host: {host}");

var factory = new ConnectionFactory { HostName = host };
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
var channelMessager = connection.CreateModel();

while(true){

    channelMessager.QueueDeclare(queue: "messager",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null);
    channel.QueueDeclare(queue: "emailer",
                        durable: false,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);

    Console.WriteLine(" [*] Waiting for messages.");

    // Working email handler
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var jsonContent = JsonConvert.DeserializeObject<EmailContent>(message);
        Console.WriteLine($" [v] Emailer: Received {jsonContent?.ToString()}");
    };

    // Base/Test consumer
    var consumerMessager = new EventingBasicConsumer(channelMessager);
    consumerMessager.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($" [x] Messager: Received {message}");
    };

    channel.BasicConsume(queue: "emailer",
                        autoAck: true,
                        consumer: consumer);

    channelMessager.BasicConsume(queue: "messager",
        autoAck: true,
        consumer: consumerMessager);
        
    Console.ReadLine();
}