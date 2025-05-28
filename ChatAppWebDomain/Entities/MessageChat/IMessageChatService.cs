using ChatAppWebDomain.Shared;
using ChatAppWebDomain.Shared.ViewModel;

namespace ChatAppWebDomain.Entities.MessageChat
{
    public interface IMessageChatService
    {
        Task<PaginationResult<MessageChatEntity>> GetList(int pageSize, int pageNumber, string? conditions = null, string? sortOrderColumn = null, string? sortOrderDirection = null, object? parameters = null);
        Task<MessageChatViewModel> GetById(int id);
        Task<MessageChatViewModel> Add(MessageChatViewModel entityAdd);
        Task<MessageChatViewModel> Edit(MessageChatViewModel entityUpdate);
        Task<bool> Delete(MessageChatViewModel entityDelete);
        Task<bool> Send(MessageChatViewModel modelView);
    }
}
