using Application.Core;
using Application.DTOs;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Logic;

public class WorkspaceLogic {
    private readonly DataContext _dataContext;
    private readonly IUserAccessor _userAccessor;

    public WorkspaceLogic(DataContext dataContext, IUserAccessor userAccessor) {
        _dataContext = dataContext;
        _userAccessor = userAccessor;
    }

    public async Task<Result<bool>> DeleteWorkspaceAsync(Guid id) {
        var usersUsername = _userAccessor.GetUsername();

        var workspace = await _dataContext.Workspaces
                                          .Include(x => x.Owner)
                                          .Where(x => x.Id == id && x.Owner!.UserName == usersUsername)
                                          .FirstOrDefaultAsync();
        
        if (workspace == null) {
            return Result<bool>.Failure("Workspace doesn't exist or you are not the owner of workspace");
        }
        _dataContext.Workspaces.Remove(workspace);
        var result = await _dataContext.SaveChangesAsync() > 0;

        if (result) {
            return Result<bool>.Success(true);
        } 

        return Result<bool>.Failure("Error trying to delete workspace");
    }

    public async Task<Result<List<ResponseWorkspaceDto>>> GetUsersWorkspacesAsync() {
        var usersUsername = _userAccessor.GetUsername();

        var workspaces = await _dataContext.Workspaces
                                           .Include(x => x.Members)
                                           .Include(x => x.Owner)
                                           .Include(x => x.Documents)
                                           .Where(x => x.Members.Any(y => y.UserName == usersUsername))
                                           .Select(x => new ResponseWorkspaceDto {
                                                Id = x.Id,
                                                Name = x.Name,
                                                OwnsWorkspace = x.Owner!.UserName == usersUsername,
                                                Documents = x.Documents.Select(y => new ResponseDocumentDto { Id = y.Id, Name = y.Name}).ToList()
                                           })
                                           .ToListAsync();
        
        return Result<List<ResponseWorkspaceDto>>.Success(workspaces);
    }

    public async Task<Result<ResponseWorkspaceDto>> CreateWorkspaceAsync(RequestWorkspaceDto workspaceDto) {
        var userUsername = _userAccessor.GetUsername();

        var existsWithSameName = await _dataContext.Workspaces
                                          .Include(x => x.Members)
                                          .CountAsync(x => x.Owner!.UserName == userUsername && x.Name == workspaceDto.Name) != 0;

        if (existsWithSameName) {
            return Result<ResponseWorkspaceDto>.Failure("Workspace with this name alredy exists");
        }

        var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == userUsername);
        if (user == null) {
            return Result<ResponseWorkspaceDto>.Failure("Couldn't identify user");
        }

        var newWorkspace = new Workspace() {
            Id = Guid.NewGuid(),
            Name = workspaceDto.Name,
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now,
            Owner = user
        };
        newWorkspace.Members.Add(user);

        await _dataContext.Workspaces.AddAsync(newWorkspace);
        var result = await _dataContext.SaveChangesAsync()  > 0;

        if (!result) {
            return Result<ResponseWorkspaceDto>.Failure("Error trying to create new workspace");
        }

        return Result<ResponseWorkspaceDto>.Success(
            new ResponseWorkspaceDto() {
                Id = newWorkspace.Id,
                Name = newWorkspace.Name,
                OwnsWorkspace = true
            }
        );
    }

    public async Task<Result<ResponseWorkspaceDto>> JoinWorkspaceAsync(Guid workspaceId) {
        var userUsername = _userAccessor.GetUsername();
        var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == userUsername);
        
        if (user == null) {
            return Result<ResponseWorkspaceDto>.Failure("Couldn't identify user");
        }

        var workspace = await _dataContext.Workspaces
                                          .Include(x => x.Documents)
                                          .Include(x => x.Members)
                                          .Where(x => x.Id == workspaceId)
                                          .FirstOrDefaultAsync();
        
        if (workspace == null) {
            return Result<ResponseWorkspaceDto>.Failure("Couldn't find workspace with the given id");
        }

        if (workspace.Members.Contains(user)) {
            return Result<ResponseWorkspaceDto>.Failure("You are alredy member of this workspace");
        }

        workspace.Members.Add(user);
        _dataContext.Update(workspace);
        var result = await _dataContext.SaveChangesAsync() > 0;

        if (result) {
            return Result<ResponseWorkspaceDto>.Success(
                new ResponseWorkspaceDto() {
                    Id = workspace.Id,
                    Name = workspace.Name,
                    OwnsWorkspace = false,
                    Documents = workspace.Documents.Select(x => new ResponseDocumentDto() {
                        Id = x.Id,
                        Name = x.Name,
                    }).ToList()
                }
            );
        }

        return Result<ResponseWorkspaceDto>.Failure("Error trying to join workspace");
    }
}