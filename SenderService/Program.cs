namespace SenderService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sender = new FileSender();
            sender.SendMessage("Hello, World!");

            Console.WriteLine("Press any key to exit...");
            Console.ReadLine();
        }
    }
}