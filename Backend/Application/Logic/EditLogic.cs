using Application.Core;
using Domain;
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
}