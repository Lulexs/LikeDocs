using API.Extensions;
using API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("CORS");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<EditHub>("/edit");

app.Run();

// [{"n":0,"m":0,"diff":[[-1,"Daburaemon"]]}]
