using Application.Core;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Logic;

public class EditLogic {
    private readonly DataContext _dataContext;

    public EditLogic(DataContext dataContext) {
        _dataContext = dataContext;
    }

    public async Task<Result<string>> GetDocContent(Guid docId) {
        var res = await _dataContext.Documents.FindAsync(docId);

        if (res == null) {
            return Result<string>.Failure("Can't find document");
        }

        return Result<string>.Success(res.Text);
    }

    public async Task<Result<bool>> CreateUserContext(UserContext userContext) {
        await _dataContext.UserContexts.AddAsync(userContext);
        var res = await _dataContext.SaveChangesAsync() > 0;

        if (res) {
            return Result<bool>.Success(true);
        } 
        return Result<bool>.Failure("Can't create user context");
    }

    public async Task<Result<bool>> DeleteUserContext(string connectionId) {
        var context = await _dataContext.UserContexts.FirstOrDefaultAsync(x => x.connectionId == connectionId);
        if (context != null) {
            _dataContext.Remove(context);
            await _dataContext.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
        return Result<bool>.Failure("Error trying to delete context");
    }
}