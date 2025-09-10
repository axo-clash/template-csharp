using System.Text.Json.Serialization;
using template_csharp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddScoped<IBotService, BotService>();

var app = builder.Build();

app.MapControllers();

app.Run();
