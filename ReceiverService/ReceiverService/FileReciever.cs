using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ReceiverService
{
    public class FileReciever
    {
        private const string QueueName = "file_queue";
        private const string LocalFolder = "C:\\Users\\Shakhriyor_Toylokov\\Documents\\IncomingMessages";
        public void StartReceiving()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            Console.WriteLine("Waiting for messages...");
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Message received: {0}", message);

                var filePath = Path.Combine(LocalFolder, $"{Guid.NewGuid()}.txt");
                File.WriteAllText(filePath, message);
            };

            channel.BasicConsume(queue: QueueName, autoAck: true, consumer: consumer);

        }
    }
}
