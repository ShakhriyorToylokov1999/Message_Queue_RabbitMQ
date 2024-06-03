using System.Text;
using RabbitMQ.Client;

namespace SenderService
{
    public class FileSender
    {
        private const string QueueName = "file_queue";
        private const string LocalFolder = "C:\\Users\\Shakhriyor_Toylokov\\Documents";
        public void SendMessage(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare(queue: QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var watcher = new FileSystemWatcher(LocalFolder, "*.pdf");
            watcher.Created += (sender, e) =>
            {
                var filePath = e.FullPath;
                var content = File.ReadAllText(filePath);

                var body = Encoding.UTF8.GetBytes(content);
                channel.BasicPublish(exchange: "", routingKey: QueueName, basicProperties: null, body: body);
            };

            watcher.EnableRaisingEvents = true;
            Console.WriteLine("Message sent: {0}", message);
        }
    }
}
