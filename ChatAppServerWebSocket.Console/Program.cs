using ChatAppServerWebSocket.Console;
using ChatAppWebDomain.Entities.MessageChat;
using ChatAppWebInfra.Ioc;
using ChatAppWebRepository.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static ChatAppServerWebSocket.Console.Server;

namespace SimpleListener
{
     class Program
    {
        static void Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            
            builder.Services.AddAutoMapper(typeof(MessageChatMapping));

            builder.Services.AddDependencies();

            builder.Build();


            try
            {
                Server server = new Server();

                server.OnMessageReceived += (object sender, OnMessageReceivedHandler e) =>
                {
                    Console.WriteLine("Message '{0}' received from client: {1} at {2}", e.GetMessage(), e.GetClient(), DateTime.Now);
                };

                server.ServerInit();

                server.Listen();
            }
            catch (Exception ex)
            {
                ///Message;
            }
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<IMessageChatService, MessageChatService>()
                .AddScoped<IMessageChatRepository, MessageChatRepository>();
        }
    }
}