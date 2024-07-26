using DiffMatchPatch;

namespace Application.DTOs;

public class EditDto {
    public int n { get; set; }
    public int m { get; set; }
    public Diff[] Diffs { get; set; } = null!;
}