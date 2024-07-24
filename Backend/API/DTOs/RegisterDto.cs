using System.ComponentModel.DataAnnotations;

namespace API.Controllers;

public class RegisterDto {
    [EmailAddress]
    public required string Email { get; set; }

    public required string Password { get; set; }
    public required string Username { get; set; }
}