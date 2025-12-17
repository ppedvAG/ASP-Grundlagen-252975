using Microsoft.AspNetCore.SignalR;

namespace M011_SignalR;

public class ChatHub : Hub
{
	public override async Task OnConnectedAsync()
	{
		await Clients.All.SendAsync("UserVerbunden");
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		await Clients.All.SendAsync("UserGetrennt");
	}

	public async Task HubNachrichtEmpfangen(string username, string msg)
	{
		//ReceiveMessage wird im Frontend im JS angegeben
		await Clients.All.SendAsync("ReceiveMessage", username, msg);
	}
}
