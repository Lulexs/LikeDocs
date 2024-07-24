using System.Security.Cryptography.X509Certificates;
using Application.Core;
using Application.DTOs;
using Application.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application;

public class WorkspaceLogic {
    private readonly DataContext _dataContext;
    private readonly IUserAccessor _userAccessor;

    public WorkspaceLogic(DataContext dataContext, IUserAccessor userAccessor) {
        _dataContext = dataContext;
        _userAccessor = userAccessor;
    }

    public async Task<Result<WorkspaceDto>> CreateWorkspaceAsync(WorkspaceDto workspaceDto) {
        var userUsername = _userAccessor.GetUsername();

        var existsWithSameName = await _dataContext.Workspaces
                                          .Include(x => x.Owner)
                                          .CountAsync(x => x.Owner!.UserName == userUsername && x.Name == workspaceDto.Name) != 0;

        if (existsWithSameName) {
            return Result<WorkspaceDto>.Failure("Workspace with this name alredy exists");
        }

        var user = await _dataContext.Users.FirstOrDefaultAsync(x => x.UserName == userUsername);
        if (user == null) {
            return Result<WorkspaceDto>.Failure("Couldn't identify user");
        }

        var newWorkspace = new Workspace() {
            Id = Guid.NewGuid(),
            Name = workspaceDto.Name,
            CreatedAt = DateTime.Now,
            LastModified = DateTime.Now,
            Owner = user
        };

        await _dataContext.Workspaces.AddAsync(newWorkspace);
        var result = await _dataContext.SaveChangesAsync()  > 0;

        if (!result) {
            return Result<WorkspaceDto>.Failure("Error trying to create new workspace");
        }

        return Result<WorkspaceDto>.Success(
            new WorkspaceDto() {
                Id = newWorkspace.Id,
                Name = newWorkspace.Name
            }
        );
    }
}