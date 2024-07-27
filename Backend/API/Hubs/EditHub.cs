using System.Text.Json;
using Application.DTOs;
using Application.Logic;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs;

public class EditHub : Hub {
    private readonly EditLogic _editLogic;

    public EditHub(EditLogic editLogic) {
        _editLogic = editLogic;
    }

    public async Task NewEdits(JsonElement editsDto) {
        var lastN = await _editLogic.ApplyEdits(Context.ConnectionId, DeserializeEdits(editsDto));
        Console.WriteLine(lastN);
    }

    private List<EditDto> DeserializeEdits(JsonElement editsDto) {
        List<EditDto> edits = new List<EditDto>();

        var editList = JsonSerializer.Deserialize<List<JsonElement>>(editsDto.GetRawText());

        foreach (var obj in editList!)
        {
            obj.TryGetProperty("n", out var n);
            obj.TryGetProperty("m", out var m);
            obj.TryGetProperty("diff", out var diff);
            var diffs = JsonSerializer.Deserialize<List<JsonElement>>(diff);

            var newEdit = new EditDto()
            {
                n = n.GetInt32(),
                m = m.GetInt32(),
                diff = []
            };


            foreach(var dif in diffs!) {
                dif.TryGetProperty("operation", out var op);
                dif.TryGetProperty("text", out var text);
                newEdit.diff.Add(new DiffMatchPatch.Diff((DiffMatchPatch.Operation)op.GetInt32(), text.GetString()));
            }

            edits.Add(newEdit);
        }

        return edits;
    }

    public override async Task OnDisconnectedAsync(Exception? exception) {
        await _editLogic.DeleteUserContext(Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    public override async Task OnConnectedAsync()
    {
        var httpContext = Context.GetHttpContext();
        var docId = httpContext!.Request.Query["docId"];

        await Groups.AddToGroupAsync(Context.ConnectionId, docId!);
        var result = await _editLogic.GetDocContent(Guid.Parse(docId!));

        if (!result.IsSuccess) {
            await Clients.Caller.SendAsync("ErrorEstablishingConnection");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, docId!);
        }

        var created = await _editLogic.CreateUserContext(Guid.Parse(docId!), new Domain.UserContext() {
                Id = Guid.NewGuid(),
                connectionId = Context.ConnectionId,
                ServerShadow = result.Value!,
                ShadowBackup = result.Value!,
            });
        
        if (!created.IsSuccess) {
            await Clients.Caller.SendAsync("ErrorEstablishingConnection");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, docId!);
        }
        else {
            await Clients.Caller.SendAsync("GetInitialState", result.Value);
        }
    }
}

