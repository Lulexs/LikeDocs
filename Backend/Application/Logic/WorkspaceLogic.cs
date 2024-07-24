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

    public async Task<Result<List<ResponseWorkspaceDto>>> GetUsersWorkspaces() {
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
                                                Documents = x.Documents.Select(y => new DocumentDto { Id = y.Id, Name = y.Name}).ToList()
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
}