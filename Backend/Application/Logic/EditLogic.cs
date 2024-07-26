using Application.Core;
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
}