namespace ReceiverService
{
    public class Program
    {
        static void Main(string[] args)
        {
            var receiver = new FileReciever();
            receiver.StartReceiving();
        }
    }
}