using Microsoft.AspNetCore.Identity;

namespace Domain;

public class AppUser : IdentityUser {
    public ICollection<Workspace> OwnWorkspaces { get; set; } = [];
    public ICollection<Workspace> MemberWorkspaces { get; set; } = [];
}