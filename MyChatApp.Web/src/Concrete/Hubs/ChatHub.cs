using Microsoft.AspNetCore.SignalR;
using MyChatApp.Service.Abstract.Managers;

namespace MyChatApp.Web.src.Concrete.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ILogger<ChatHub> _logger;
        private readonly IMessageManager _messageManager;

        public ChatHub(IMessageManager messageManager, ILogger<ChatHub> logger)
        {
            _messageManager = messageManager;
            _logger = logger;
        }

        public async Task JoinGroup(string group)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, group);
        }

        public async Task SendMessage(string message, string sender)
        {
            await Clients.All.SendAsync("SendMessageEveryoneListener", sender, message);
        }

        public async Task SendGroupMessage(string groupName, string message, string sender)
        {
            try
            {
                await _messageManager.AddAsync(new()
                {
                    Channel = groupName,
                    Content = message,
                    UserName = sender
                });
                await Clients.Group(groupName).SendAsync("GroupMessageListener", sender, message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }            
        }
    }
}
