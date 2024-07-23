// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.

// builder.Services.AddControllers();
// // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

// app.UseAuthorization();

// app.MapControllers();

// app.Run();


using DiffMatchPatch;

diff_match_patch dmp = new();

string str1 = "Hello World";
string str2 = "Hello World but in Mexican";

List<Diff> diffs = dmp.diff_main(str1, str2);

List<Patch> patch = dmp.patch_make(diffs);

// foreach (var v in diffs) {
//     Console.WriteLine(v);
// }

foreach (var p in patch) {
    Console.WriteLine(p);
}

var obj = dmp.patch_apply(patch, str1);

