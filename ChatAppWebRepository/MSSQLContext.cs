using ChatAppWebDomain.Entities.MessageChat;
using Microsoft.EntityFrameworkCore;

namespace ChatAppWebRepository
{
    public class MSSQLContext : DbContext
    {
        public MSSQLContext(DbContextOptions<MSSQLContext> options) : base(options) { }

        public DbSet<MessageChatEntity> MessageChat { get; set; }
    }
}
