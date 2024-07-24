using Microsoft.AspNetCore.Identity;

namespace Domain;

public class AppUser : IdentityUser {
    public ICollection<Workspace> Workspaces { get; set; } = [];
    public UserContext? UserContext { get; set; }
}