using DiffMatchPatch;

namespace Application.DTOs;

public class EditDto {
    public int n { get; set; }
    public int m { get; set; }
    public List<Diff> diff { get; set; } = [];
}