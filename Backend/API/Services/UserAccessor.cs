using System.Security.Claims;
using Application.Interfaces;

namespace API.Services;

public class UserAccessor : IUserAccessor {
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor) {
        _httpContextAccessor = httpContextAccessor;
    }
    public string GetUsername() {
        return _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Name)!;
    }
}