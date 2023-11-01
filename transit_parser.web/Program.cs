using transit_parser.Controllers;
using transit_parser.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSingleton<TransitService>();

/* TLDR
Transient objects are always different; a new instance is provided to every controller and every service.
Scoped objects are the same within a request, but different across different requests.
Singleton objects are the same for every object and every request.
*/

var app = builder.Build();

// Linux disable
// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
