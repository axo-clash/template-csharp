using template_csharp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IBotService, BotService>();

var app = builder.Build();

app.MapControllers();

app.Run();
