using Application.Core;
using Application.DTOs;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Logic;

public class DocumentLogic {
    private readonly DataContext _dataContext;
    private readonly IUserAccessor _userAccessor; 

    public DocumentLogic(DataContext dataContext, IUserAccessor userAccessor) {
        _dataContext = dataContext;
        _userAccessor = userAccessor;
    }

    public async Task<Result<bool>> DeleteDocument(Guid documentId) {
        var usersUsername = _userAccessor.GetUsername();

        var document = await _dataContext.Documents
                                         .Include(x => x.Workspace)
                                         .ThenInclude(x => x!.Members)
                                         .Where(x => x.Id == documentId && x.Workspace!.Members.Any(y => y.UserName == usersUsername))
                                         .FirstOrDefaultAsync();

        if (document == null) {
            return Result<bool>.Failure("Document not found");
        }

        _dataContext.Documents.Remove(document);
        var result = await _dataContext.SaveChangesAsync() > 0;

        if (result) {
            return Result<bool>.Success(true);
        }

        return Result<bool>.Failure("Error trying to delete document");
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