using API.DTOs;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase {
    private readonly UserManager<AppUser> _userManager;
    private readonly TokenService _tokenService;

    public AccountController(UserManager<AppUser> userManager, TokenService tokenService) {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody]LoginDto loginDto) {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user == null) { return Unauthorized(); }

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (result) {
            return Ok(new UserDto {
                Username = user.UserName!,
                Email = user.Email!,
                Token = _tokenService.CreateToken(user)
            });
        }

        return Unauthorized();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody]RegisterDto registerDto) {
        if (await _userManager.FindByEmailAsync(registerDto.Email) != null) {
            return BadRequest("Email taken");
        }
        if (await _userManager.FindByNameAsync(registerDto.Username) != null) {
            return BadRequest("Username taken");
        }

        var user = new AppUser() {
            UserName = registerDto.Username,
            Email = registerDto.Email,
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (result.Succeeded) {
                return Ok(new UserDto {
                Username = user.UserName!,
                Email = user.Email!,
                Token = _tokenService.CreateToken(user)
            });
        }

        return BadRequest(result.Errors);
    }

}