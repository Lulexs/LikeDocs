using System.ComponentModel.DataAnnotations;

namespace Domain;

public class DiffWrapper {
    [Key]
    public int Id { get; set; }
    public int operation { get; set; }
    public required string text { get; set; }
    // public Edit? Edit { get; set; }
}