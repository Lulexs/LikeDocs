using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Edit {
    [Key]
    public int Id { get; set; } 
    public int n { get; set; }
    public int m { get; set; }
    public List<DiffWrapper> diff { get; set; } = [];
    // public UserContext? UserContext { get; set; }
}