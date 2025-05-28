using ChatAppWebDomain.Shared.ViewModel;
using ChatAppWebDomain.Shared;

namespace ChatAppWebDomain.Entities.MessageChat
{
    public interface IMessageChatRepository
    {
        Task<PaginationResult<MessageChatEntity>> GetList(int pageSize, int pageNumber, string? conditions = null, string? sortOrderColumn = null, string? sortOrderDirection = null, object? parameters = null);
        Task<MessageChatEntity?> GetById(int Id);
        Task<MessageChatEntity> Add(MessageChatEntity entityAdd);
        Task<MessageChatEntity> Edit(MessageChatEntity entityUpdate);
        Task<bool> Delete(MessageChatEntity entityDelete);
    }
}
