using System.Net.Sockets;
using System.Net;
using System.Text;
using ChatAppClientWebSocket.Console.Model;
using System.Text.Json;


class Program
{
    static void Main(string[] args)
    {

        MessageModel message = new MessageModel()
        {
            Id = 1,
            Source = "samcoelho@yahoo.com.br",
            Destination = "samdrive1903@gmail.com",
            Body = "Menssage sent to WebSocket from client socket",
            IsRead = false,
            SentOn = DateTime.Now,
        };

        string jsonString = JsonSerializer.Serialize(message);

        Console.WriteLine($"Socket client sent message: \"{message.Body}\"");

        IPEndPoint ipEndPoint = new(IPAddress.Parse("127.0.0.1"), 80);

        using Socket client = new(
            ipEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);

        client.ConnectAsync(ipEndPoint);

        while (true)
        {
            var messageBytes = Encoding.UTF8.GetBytes(jsonString);
            _ = client.SendAsync(messageBytes, SocketFlags.None);
            Console.WriteLine($"Socket client sent message: \"{message.Body}\"");

            // Receive ack.  
             var buffer = new byte[1_024];
            var received = client.ReceiveAsync(buffer, SocketFlags.None);
            var response = Encoding.UTF8.GetString(buffer, 0, 10);
            if (response != null)
            {
                Console.WriteLine(
                    $"Socket client received : \"{response}\"");
                break;
            }
        }

        client.Shutdown(SocketShutdown.Both);

    }

}




