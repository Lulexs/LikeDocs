using Application;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class WorkspacesController : BaseApiController {
    private readonly WorkspaceLogic _workspaceLogic;

    public WorkspacesController(WorkspaceLogic workspaceLogic) {
        _workspaceLogic = workspaceLogic;
    }

    [HttpPost("create")]
    public async Task<ActionResult<WorkspaceDto>> CreateWorkspace([FromBody]WorkspaceDto workspaceDto) {
        return HandleResult(await _workspaceLogic.CreateWorkspaceAsync(workspaceDto));
    } 
}