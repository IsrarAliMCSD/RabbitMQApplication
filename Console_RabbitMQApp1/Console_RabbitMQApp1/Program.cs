using RabbitMQ.Client;
using System.Text;

namespace Console_RabbitMQApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                // Declare a queue
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                string message = "Hello RabbitMQ!";
                var body = Encoding.UTF8.GetBytes(message);

                // Publish a message
                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);
                int i = 0;
                while (i < 10)
                {
                    message = "Hello RabbitMQ!" + i;
                    body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                                   routingKey: "hello",
                                   basicProperties: null,
                                   body: body);
                    Thread.Sleep(10000);
                    i++;
                }
                Console.WriteLine($" [x] Sent {message}");
            }
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
