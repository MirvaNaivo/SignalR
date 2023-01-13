
using Microsoft.AspNetCore.SignalR;
using BlazorSignalRApp.Shared;
using BlazorSignalRApp.Server.Data;

namespace BlazorSignalRApp.Server.Hubs;

public class ChatHub : Hub
{
    private readonly AppDataContext _db;

    public ChatHub(AppDataContext db)
    {
        _db = db;
    }

    public async Task PostMessage(ChatMessage message)
    {
        ChatMessageNotification notification = new ChatMessageNotification
        {
            Message = message.Message,
            User = message.User,
            MessageTime = DateTime.Now
        };

        _db.Messages.Add(notification);
        await MessageNotification(notification);
        
    }

    public async Task MessageNotification(ChatMessageNotification notification)
    {
        await Clients.All.SendAsync("ReceiveMessage", notification);
    }
}
