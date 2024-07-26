using Application.Logic;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class EditHub : Hub {
    private readonly EditLogic _editLogic;

    public EditHub(EditLogic editLogic) {
        _editLogic = editLogic;
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var docId = httpContext!.Request.Query["docId"];
        await Groups.AddToGroupAsync(Context.ConnectionId, docId!);
        var result = await _editLogic.GetDocContent(Guid.Parse(docId!));
        if (result.IsSuccess) {
            await Clients.Caller.SendAsync("GetInitialState", result.Value);
        }
        else {
            await Clients.Caller.SendAsync("ErrorEstablishingConnection");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, docId!);
        }
    }
}

