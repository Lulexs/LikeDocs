using Application.Logic;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WorkspacesController : BaseApiController {
    private readonly WorkspaceLogic _workspaceLogic;

    public WorkspacesController(WorkspaceLogic workspaceLogic) {
        _workspaceLogic = workspaceLogic;
    }

    [HttpPost("")]
    public async Task<ActionResult<ResponseWorkspaceDto>> CreateWorkspace([FromBody]RequestWorkspaceDto workspaceDto) {
        return HandleResult(await _workspaceLogic.CreateWorkspaceAsync(workspaceDto));
    }

    [HttpPost("join/{workspaceId:guid}")]
    public async Task<ActionResult<ResponseWorkspaceDto>> JoinWorkspace(Guid workspaceId) {
        return HandleResult(await _workspaceLogic.JoinWorkspaceAsync(workspaceId));
    }

    [HttpGet("")]
    public async Task<ActionResult<List<ResponseWorkspaceDto>>> GetWorkspaces() {
        return HandleResult(await _workspaceLogic.GetUsersWorkspacesAsync());
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<bool>> DeleteWorkspace(Guid id) {
        return HandleResult(await _workspaceLogic.DeleteWorkspaceAsync(id));
    }
}