using Application.Core;
using Application.DTOs;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Logic;

public class DocumentLogic {
    private readonly DataContext _dataContext;

    public DocumentLogic(DataContext dataContext) {
        _dataContext = dataContext;
    }

    public async Task<Result<ResponseDocumentDto>> CreateDocument(Guid workspaceId, RequestDocumentDto documentDto) {
        var workspace = await _dataContext.Workspaces
                                          .Include(x => x.Documents)
                                          .Where(x => x.Id == workspaceId)
                                          .Select(x => new { 
                                            obj = x,
                                            docCount = x.Documents.Count()
                                          })
                                          .FirstOrDefaultAsync();

        if (workspace == null) {
            return Result<ResponseDocumentDto>.Failure("Workspace not found");
        }

        if (workspace.docCount == 5) {
            return Result<ResponseDocumentDto>.Failure("Cannot create more than 5 documents in one workspace");
        }

        var newDocument = new Document() {
            Id = Guid.NewGuid(),
            Name = documentDto.Name,
            Text = string.Empty,
            LastModified = DateTime.Now,
            CreatedAt = DateTime.Now,
            Workspace = workspace.obj,
        };

        await _dataContext.Documents.AddAsync(newDocument);
        var result = await _dataContext.SaveChangesAsync() > 0;

        if (result) {
            return Result<ResponseDocumentDto>.Success(new ResponseDocumentDto() {
                Id = newDocument.Id,
                Name = newDocument.Name
            });
        }

        return Result<ResponseDocumentDto>.Failure("Error trying to create new document");
    }
}