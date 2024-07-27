using Application.Core;
using Application.DTOs;
using DiffMatchPatch;
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

    public async Task<Result<bool>> CreateUserContext(Guid docId, UserContext userContext) {
        var doc = await _dataContext.Documents.FindAsync(docId);
        userContext.Document = doc;
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

    public async Task<Result<List<Edit>>> ApplyEditsAndPrepareEditsForSending(string connectionId, List<EditDto> edits) {
        var context = await _dataContext.UserContexts
                                        .Include(x => x.Document)
                                        .Where(x => x.connectionId == connectionId)
                                        .FirstOrDefaultAsync();

        if (context == null) 
            return Result<List<Edit>>.Failure("Couldn't find user's context");
        
        await ApplyEdits(context, edits);
        await PrepareEditsForSending(context);

        return Result<List<Edit>>.Success(context.Edits);
    }

    private async Task PrepareEditsForSending(UserContext context) {
        var dmp = new diff_match_patch();

        var serverText = context.Document!.Text;
        var diffs = dmp.diff_main(context.ServerShadow, serverText);
        
        var edit = new Edit() {
            n = context.N,
            m = context.M,
            diff = diffs.Select(x => new DiffWrapper() { text = x.text, operation = (int)x.operation }).ToList()
        };

        if (edit.diff.Count > 0) {
            context.ServerShadow = serverText;
            context.M += 1;

            context.Edits.Add(edit);
            _dataContext.Update(context);
            await _dataContext.SaveChangesAsync();
        }
    }

    private async Task<Result<int>> ApplyEdits(UserContext context, List<EditDto> edits) {
        if (edits.Count == 0)
            return Result<int>.Success(-1);

        var dmp = new diff_match_patch();

        edits = edits.Where(x => x.n >= context.N).ToList();

        if (edits.First().m < context!.M) {
            context.ServerShadow = context.ShadowBackup;
            context.M = context.BackupM;
            context.Edits.Clear();
        }

        var diffs = edits.Where(edit => context.N <= edit.n).Select(x => x.diff).SelectMany(list => list).ToList();
        var patches = dmp.patch_make(diffs);

        context.ServerShadow = (string)dmp.patch_apply(patches, context.ServerShadow)[0];
        context.N += edits.Count;
        context.ShadowBackup = context.ServerShadow;
        context.BackupM = context.M;

        var appliedPatch = dmp.patch_apply(patches, context.Document!.Text)[0];
        context.Document.Text = (string)appliedPatch;

        _dataContext.Update(context);
        context.Edits = context.Edits.Where(x => x.m < edits.Last().m).ToList();
        await _dataContext.SaveChangesAsync();

        return Result<int>.Success(edits.Last().n);
    }
}