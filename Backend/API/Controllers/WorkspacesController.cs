using Application.Logic;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WorkspacesController : BaseApiController {
    private readonly WorkspaceLogic _workspaceLogic;

    public WorkspacesController(WorkspaceLogic workspaceLogic) {
        _workspaceLogic = workspaceLogic;
    }

    [HttpPost("create")]
    public async Task<ActionResult<ResponseWorkspaceDto>> CreateWorkspace([FromBody]RequestWorkspaceDto workspaceDto) {
        return HandleResult(await _workspaceLogic.CreateWorkspaceAsync(workspaceDto));
    } 

    [HttpGet("")]
    public async Task<ActionResult<List<ResponseWorkspaceDto>>> GetWorkspaces() {
        return HandleResult(await _workspaceLogic.GetUsersWorkspaces());
    }
}