using API.Services;
using Application;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions;

public static class ApplicationServiceExtensions {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config) {
        
        services.AddDbContext<DataContext>(options => {
            string connString = config.GetConnectionString("DefaultConnection")!;
            options.UseSqlite(connString);
        });
        
        services.AddCors(opt => {
            opt.AddPolicy("CORS", policy => {
                policy.AllowCredentials()
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .WithOrigins(["http://localhost:5173", "http://127.0.0.1:5173"]);
            });
        });

        services.AddSignalR();
        services.AddControllers(opt => {
            var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            opt.Filters.Add(new AuthorizeFilter(policy));
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<WorkspaceLogic>();

        return services;
    }
}