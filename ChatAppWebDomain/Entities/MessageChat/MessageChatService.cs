using AutoMapper;
using ChatAppWebDomain.Shared;
using ChatAppWebDomain.Shared.ViewModel;
using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using System.Text;

namespace ChatAppWebDomain.Entities.MessageChat
{
    public class MessageChatService(IMessageChatRepository messageChatRepository, IMapper _mapper) : IMessageChatService
    {
        public async Task<MessageChatViewModel> Add(MessageChatViewModel messageChatViewModel)
        {
            SetAddRulles(ref messageChatViewModel);

            var entityAdd = _mapper.Map<MessageChatEntity>(messageChatViewModel);

            return _mapper.Map<MessageChatViewModel>(await messageChatRepository.Add(entityAdd));
        }

        public async Task<MessageChatViewModel> Edit(MessageChatViewModel messageChatViewModel)
        {
            var entityEdit = _mapper.Map<MessageChatEntity>(messageChatViewModel);

            return _mapper.Map<MessageChatViewModel>(await messageChatRepository.Edit(entityEdit));

        }

        public async Task<bool> Delete(MessageChatViewModel messageChatViewModel)
        {
            var entityDelete = _mapper.Map<MessageChatEntity>(messageChatViewModel);

            return await messageChatRepository.Delete(entityDelete);

        }

        public async Task<MessageChatViewModel> GetById(int id)
        {

            var viewModel = _mapper.Map<MessageChatViewModel>(await messageChatRepository.GetById(id));

            return viewModel;
        }

        public async Task<PaginationResult<MessageChatEntity>> GetList(int pageSize, int pageNumber, string? conditions, string? sortOrderColumn, string? sortOrderDirection, object? parameters = null)
        {

            return await messageChatRepository.GetList(pageSize, pageNumber, conditions, sortOrderColumn, sortOrderDirection, null);
        }

        private void SetAddRulles(ref MessageChatViewModel entityAdd)
        {
            SetAddDefaulValues(ref entityAdd);
        }

        private void SetAddDefaulValues(ref MessageChatViewModel entityAdd)
        {
            entityAdd.SentOn = DateTime.Now;
            entityAdd.IsRead = false;
            entityAdd.Id = null;
        }

        public Task<bool> Send(MessageChatViewModel modelView)
        {
            string jsonString = JsonSerializer.Serialize(modelView);

            Console.WriteLine($"Socket client sent message: \"{modelView.Body}\"");

            IPEndPoint ipEndPoint = new(IPAddress.Parse("127.0.0.1"), 80);

            using Socket client = new(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            client.ConnectAsync(ipEndPoint);

            var messageBytes = Encoding.UTF8.GetBytes(jsonString);
            _ = client.SendAsync(messageBytes, SocketFlags.None);
            Console.WriteLine($"Socket client sent message: \"{modelView.Body}\"");

                var buffer = new byte[1_024];
                var received = client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, 10);
                if (response != null)
                {
                    Console.WriteLine(
                        $"Socket client received : \"{response}\"");
                }

            client.Shutdown(SocketShutdown.Both);

            return Task.FromResult(true); ;
        }
    }
}
