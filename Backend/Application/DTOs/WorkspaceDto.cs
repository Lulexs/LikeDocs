using Domain;

namespace Application.DTOs;

public class WorkspaceDto {
    public Guid? Id { get; set; } = null;
    public required string Name { get; set; }
    public ICollection<Document> Documents { get; set; } = [];

}