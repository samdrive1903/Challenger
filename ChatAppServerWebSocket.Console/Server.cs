using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using ChatAppWebDomain.Shared.ViewModel;
using System.Net.Http.Headers;
using System.Net.Mime;
using ChatAppWebDomain.Entities.MessageChat;

namespace ChatAppServerWebSocket.Console
{
    public class Server
    {
        const int PORT = 80;
        const int BACKLOG = 5;

        static Socket socket;
        private static byte[] rcvBuffer;
        private static int bytesRcvd;
        private static string message;

        private readonly IMessageChatService _messageChatService;
        public Server() { }

        public Server(IMessageChatService messageChatService) 
        { 
            _messageChatService = messageChatService;
        }

        public void ServerInit()
        {
            try
            {
                IPEndPoint Ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 80);

                //Init Socket

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socket.Bind(Ep);
                socket.Listen(BACKLOG);

                ShowMessage("I'm listenning now..");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Listen()
        {
            try
            {
                rcvBuffer = new Byte[256];
                bytesRcvd = 0;
                message = string.Empty;

                //Start listnening
                while (true)
                {
                    var client = socket.Accept();
                    ShowMessage("Client with IP " + client.RemoteEndPoint.ToString() + " connected!");

                    //Client has connected, keep receiving/displaying data
                    while (true)
                    {
                        SocketError rcvErrorCode = 0;

                        bytesRcvd = 0;
                        message = string.Empty;

                        bytesRcvd = client.Receive(rcvBuffer, 0, rcvBuffer.Length, SocketFlags.None);

                        if (rcvErrorCode != SocketError.Success)
                        {
                            System.Console.WriteLine("Client with IP " + client.RemoteEndPoint.ToString() + " disconnected!");
                            break;
                        }

                        ReceiveMessage(bytesRcvd, client.RemoteEndPoint.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ShowMessage(string message)
        {
            System.Console.WriteLine(message);
            System.Console.WriteLine();
        }

        public void ReceiveMessage(int bytesRcvd, string client)
        {
            if (OnMessageReceived == null) throw new Exception("Server error: event OnMessageReceived is not bound!");

            message = Encoding.ASCII.GetString(rcvBuffer, 0, bytesRcvd);

            //var retorno = AddMessageByHttp(message);
            var retorno = AddMessage(message);

            OnMessageReceived(this, new OnMessageReceivedHandler(message, client));
        }


        public class OnMessageReceivedHandler : EventArgs
        {
            private string _message;
            private string _client;

            public OnMessageReceivedHandler(string Message, string Cliente)
            {
                this._message = Message;
                this._client = Cliente;
            }

            public string GetMessage()
            {
                return _message;
            }

            public string GetClient()
            {
                return _client;
            }

        }

        public event EventHandler<OnMessageReceivedHandler> OnMessageReceived;

        private async Task<bool> AddMessageByHttp(string message)
        {
            try
            {
                var modelView = JsonSerializer.Deserialize<MessageChatViewModel>(message);

                HttpClient httpClient = new HttpClient();
                var url = "http://localhost:7280/api/ChatApp/Add";
                var content = new StringContent(message, Encoding.UTF8, MediaTypeNames.Application.Json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var result = await httpClient.PostAsync(url, content);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<bool> AddMessage(string message)
        {
            try
            {

                //var serviceCollection = new ServiceCollection();
                //Program.ConfigureServices(serviceCollection);
                //var serviceProvider = serviceCollection.BuildServiceProvider();
                //var eventService = serviceProvider.GetService<IMessageChatService>();

                var modelView = JsonSerializer.Deserialize<MessageChatViewModel>(message);

                var response = await _messageChatService.Add(modelView);

                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
