using Application.Core;
using Application.DTOs;
using Application.Logic;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class DocumentsController : BaseApiController {
    private readonly DocumentLogic _documentLogic;

    public DocumentsController(DocumentLogic documentLogic) {
        _documentLogic = documentLogic;
    }

    [HttpPost("{workspaceId:guid}")]
    public async Task<ActionResult<ResponseDocumentDto>> CreateDocument(Guid workspaceId, [FromBody]RequestDocumentDto requestDocumentDto) {
        return HandleResult(await _documentLogic.CreateDocument(workspaceId, requestDocumentDto));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<bool>> DeleteDocument(Guid id) {
        return HandleResult(await _documentLogic.DeleteDocument(id));
    }
} 