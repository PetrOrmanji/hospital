using System.Reflection;
using Hospital.Db.Extensions;
using Hospital.Extensions;
using Hospital.Options;

SetCurrentDirectory();

var builder = WebApplication.CreateBuilder(args);
builder.AddSerilog();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddDb(builder.Configuration["DbConnectionString"]!);
builder.Services.AddSwagger();

builder.Services.AddMapperProfiles();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void SetCurrentDirectory()
{
    var entryAssembly = Assembly.GetEntryAssembly();
    if (entryAssembly is null)
    {
        return;
    }

    var assemblyDirectory = Path.GetDirectoryName(entryAssembly.Location);
    if (assemblyDirectory is null)
    {
        return;
    }

    Directory.SetCurrentDirectory(assemblyDirectory);
}
