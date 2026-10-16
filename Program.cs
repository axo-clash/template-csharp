using System.Text.Json.Serialization;
using template_csharp.Converters;
using template_csharp.Models;
using template_csharp.Services;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "9000";
builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddSingleton<IBotService, BotService>();

var app = builder.Build();

// JSON explicite via le contexte source-genere : l'application est compilee
// en NativeAOT, la reflexion n'est donc pas disponible. Les JsonTypeInfo
// doivent etre passes explicitement, en entree comme en sortie.
app.MapGet("/api/v1/bots/info", (IBotService botService) =>
    Results.Json(botService.GetBotInfo(), BotJsonContext.Default.BotInfoDTO));

app.MapPost("/api/v1/bots/play", async (HttpRequest request, IBotService botService) =>
{
    var gameBotDto = await request.ReadFromJsonAsync(BotJsonContext.Default.GameBotDTO);
    if (gameBotDto is null)
        return Results.BadRequest();

    var playResponse = botService.Play(gameBotDto);
    return Results.Json(playResponse, BotJsonContext.Default.PlayResponseDTO);
});

app.Run();

// Contexte de serialisation source-genere (requis en NativeAOT).
// Tout nouveau DTO ou enum echange via l'API doit y etre declare :
// - [JsonSerializable(typeof(...))] pour chaque type racine ;
// - un JsonStringEnumConverter<T> par nouvel enum.
[JsonSourceGenerationOptions(
    PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase,
    Converters = new[] {
        typeof(JsonStringEnumConverter<ActionType>),
        typeof(JsonStringEnumConverter<BotState>),
        typeof(JsonStringEnumConverter<BotType>),
        typeof(JsonStringEnumConverter<CaseContent>),
        typeof(JsonStringEnumConverter<Winner>),
        typeof(JsonStringEnumConverter<EndReason>),
        typeof(JsonStringEnumConverter<FailureReason>),
        typeof(JsonStringEnumConverter<GameStatus>),
        typeof(JsonStringEnumConverter<ItemType>),
        typeof(UnixTimestampConverter),
    })]
[JsonSerializable(typeof(BotInfoDTO))]
[JsonSerializable(typeof(GameBotDTO))]
[JsonSerializable(typeof(PlayResponseDTO))]
internal partial class BotJsonContext : JsonSerializerContext;
