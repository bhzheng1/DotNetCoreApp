// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");
var factory = new ConnectionFactory() {HostName="localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel())
{
    channel.QueueDeclare("hello",false,false,false,null);
    var consumer = new EventingBasicConsumer(channel);
    consumer.Received += (model, ea) =>
    {
        var body = ea.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"[x] Recieved {message}");
    };

    channel.BasicConsume("hello", true, consumer);

    Console.WriteLine("Press [enter] to exit.");
    Console.ReadLine();
}