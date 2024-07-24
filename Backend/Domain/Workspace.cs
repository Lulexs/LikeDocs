namespace Domain;

public class Workspace {
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }
    public ICollection<Document> Documents { get; set; } = [];
}