using ChatAppWebDomain.Entities.MessageChat;
using ChatAppWebRepository;
using ChatAppWebRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatAppWebInfra.Ioc
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services)
        {
            services
                .ChatAppWebMessage()
                .ChatAppWebDb();

            return services;

        }

        private static IServiceCollection ChatAppWebMessage(this IServiceCollection services)
        {
            services
                .AddScoped<IMessageChatService, MessageChatService>()
                .AddScoped<IMessageChatRepository, MessageChatRepository>();

            return services;
        }

        private static IConfiguration configuration { get; }

        private static IServiceCollection ChatAppWebDb(this IServiceCollection services)
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Challenger;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;";

            services.AddDbContextFactory<MSSQLContext>(options => options
                .UseSqlServer(connectionString));
            //m => m.MigrationsAssembly("ChatAppWebRepository")));

            return services;
        }

    }
}
