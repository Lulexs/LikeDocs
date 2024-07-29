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



// using DiffMatchPatch;

// string cs = "CAT";
// string ct = "CATS";

// var dmp = new diff_match_patch();

// var diffs = dmp.diff_main(cs, ct);
// var patches = dmp.patch_make(diffs);
// cs = (string)dmp.patch_apply(patches, cs)[0];

// Console.WriteLine(cs);