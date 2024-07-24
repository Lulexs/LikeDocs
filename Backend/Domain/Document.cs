namespace Domain;

public class Document {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }
    public required string Text { get; set; }
    public Workspace? Workspace { get; set; }
    public ICollection<UserContext> UserContexts { get; set; } = [];
}