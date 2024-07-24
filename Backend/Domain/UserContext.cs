using System.ComponentModel.DataAnnotations.Schema;

namespace Domain;

public class UserContext {
    public Guid Id { get; set; }
    public Document? Document { get; set; }
    
    [ForeignKey("UserContext")]
    public AppUser? AppUser { get; set; }
    public required string ServerShadow { get; set; }
    public required string ShadowBackup { get; set; }
    public int N { get; set; }
    public int M { get; set; }
    public int BackupM { get; set; }

}