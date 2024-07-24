namespace Domain;

public class Workspace {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }
    public AppUser? Owner { get; set; }
    public ICollection<AppUser> Members { get; set; } = [];
    public ICollection<Document> Documents { get; set; } = [];
}