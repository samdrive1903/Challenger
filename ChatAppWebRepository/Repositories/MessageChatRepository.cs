using ChatAppWebDomain.Entities.MessageChat;
using ChatAppWebDomain.Shared;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ChatAppWebRepository.Repositories
{
    public class MessageChatRepository : IMessageChatRepository
    {
        protected string _connectString;
        private readonly MSSQLContext _context;

        public MessageChatRepository(MSSQLContext dbContext)
        {
            _context = dbContext;
            _connectString = _context.Database.GetDbConnection().ConnectionString;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<MessageChatEntity> Add(MessageChatEntity entityAdd)
        {
            try
            {
                await _context.AddAsync(entityAdd);

                int result = await _context.SaveChangesAsync();

                return await GetById(entityAdd.Id.Value);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<MessageChatEntity> Edit(MessageChatEntity entityUpdate)
        {
            try
            {
                _context.Entry(entityUpdate).State = EntityState.Modified;

                int result = await _context.SaveChangesAsync();

                return await GetById(result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<bool> Delete(MessageChatEntity entityDelete)
        {
            try
            {
                _context.MessageChat.Remove(entityDelete);

                var result = await _context.SaveChangesAsync();

                return (result > 0);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MessageChatEntity?> GetById(int Id)
        {
            try
            {
                var messagesChat = await _context.MessageChat
                    .FromSql($"SELECT * FROM dbo.messageChat ")
                    .Where(x => x.Id == Id)
                    .AsNoTracking()
                    .FirstOrDefaultAsync();

                 return messagesChat;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginationResult<MessageChatEntity>> GetList(int pageSize, int pageNumber, string conditions, string sortOrderColumn, string sortOrderDirection, object? parameters)
        {
            try
            {
                using (var conn = new SqlConnectionHelper(_connectString))
                {
                    using (var result = new PaginationResult<MessageChatEntity>())
                    {

                        var items = await _context.MessageChat.OrderBy(o => o.SentOn).ToListAsync();                        
                        var totalRowCounts = items.Count(); ;

                        items = items.OrderBy(o => o.SentOn).Skip(((pageNumber - 1) * pageSize)).Take(pageSize).ToList();

                        result.Rows = items;
                        result.TotalRowCount = totalRowCounts;
                        result.PageCount = pageNumber;
                        result.TotalPages = totalRowCounts / pageSize + (totalRowCounts % pageSize > 0 ? 1 : 0);
                        return result;
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
