// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using System.Text;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory() { HostName = "localhost" };
using (var connection = factory.CreateConnection())
using (var channel = connection.CreateModel()) {
    channel.QueueDeclare("hello",false,false,false,null);
    var message = "Hello Wrold";
    var body = Encoding.UTF8.GetBytes(message);
    channel.BasicPublish("","hello",false,null,body);
    Console.WriteLine($"[x] Sent to {message}");
}
Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();
