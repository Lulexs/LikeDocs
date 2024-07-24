using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class LoginDto {
    [EmailAddress]
    public required string Email { get; set; }
    public required string Password { get; set; }
}