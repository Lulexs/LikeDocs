namespace Application.DTOs;

public class ResponseWorkspaceDto {
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<ResponseDocumentDto> Documents { get; set; } = [];
    public bool OwnsWorkspace { get; set; } 
}