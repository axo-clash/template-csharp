# C# Bot Template for Axo Clash

This is a C# ASP.NET Core template for creating bots that compete in the Axo Clash game competition.

## Quick Start

### Prerequisites
- .NET 10.0 SDK

### Running the Bot

1. **Start the bot server:**
   ```bash
   dotnet run
   ```
   
   The bot will start on `http://localhost:9000`

   The listen port can be overridden with the `PORT` environment variable:
   ```bash
   PORT=8080 dotnet run
   ```

2. **Verify it's working:**
   - GET `http://localhost:9000/api/v1/bots/info` - Returns bot information
   - POST `http://localhost:9000/api/v1/bots/play` - Accepts game state and returns bot action

### Docker

Build the image:
```bash
docker build -t axoclash-bot-csharp .
```

Run the container:
```bash
docker run -p 9000:9000 axoclash-bot-csharp
```

The container listens on port 9000 by default. Override it with the `PORT` environment variable (and map the port accordingly):
```bash
docker run -e PORT=8080 -p 8080:8080 axoclash-bot-csharp
```

The final image is built `FROM scratch`: the app is compiled with **NativeAOT** into a static ~16MB binary, and the image contains only that binary plus the musl loader (no OS, no runtime, no libraries).

## NativeAOT and JSON serialization

This template is compiled with **NativeAOT** (`PublishAot`), so reflection is not available at runtime. JSON serialization relies on the source-generated `BotJsonContext` declared at the bottom of `Program.cs`.

If you add a new DTO or enum exchanged via the API, you must declare it in `BotJsonContext`:
- one `[JsonSerializable(typeof(...))]` per root type;
- one `JsonStringEnumConverter<T>` per new enum.

Forgetting a registration raises an error at runtime on the affected endpoint.

## Implementing Your Bot Strategy

To customize your bot's behavior, modify the `Play` method in **`Services/BotService.cs`**:

```csharp
public PlayResponseDTO Play(GameBotDTO gameBotDto)
{
    // TODO: Implement your bot strategy here
    // Access game state: gameBotDto.Game
    // Access your bot data: gameBotDto.Bot

    // Current implementation: random action
    var random = new Random();
    var actions = Enum.GetValues<ActionType>();
    var randomAction = actions[random.Next(actions.Length)];

    return new PlayResponseDTO
    {
        Action = randomAction
    };
}
```

### Available Actions
- **Movement**: `MOVE_LEFT`, `MOVE_RIGHT`, `MOVE_UP`, `MOVE_DOWN`
- **Shooting**: `SHOOT_LEFT`, `SHOOT_RIGHT`, `SHOOT_UP`, `SHOOT_DOWN`
- **Unknown**: `UNKNOWN` (fallback action)

### Game Data Available
- **`gameBotDto.Game`**: Current game state including board, turn info, and game status
- **`gameBotDto.Bot`**: Your bot's current data and status
- **`gameBotDto.Game.Board`**: 2D array representing the game board
- **`gameBotDto.Game.Actions`**: History of previous actions

## Project Structure

```
├── Services/
│   ├── IBotService.cs       # Service interface
│   └── BotService.cs        # MODIFY THIS FILE
├── Models/                  # Data transfer objects
├── Converters/              # Custom JSON converters
├── Program.cs              # Minimal API endpoints + JSON context
├── Dockerfile               # Docker image build (NativeAOT, scratch)
├── .dockerignore            # Files excluded from the Docker build context
└── Properties/
    └── launchSettings.json # Development configuration
```

## Development

- **Hot reload**: `dotnet watch run`
- **Build**: `dotnet build`
- **Clean**: `dotnet clean`

## Bot Information

Update your bot's name and version in `BotService.GetBotInfo()`:

```csharp
return new BotInfoDTO
{
    Name = "Your Bot Name",
    Version = "1.0.0"
};
```


## Pièges et zone rouge

Les modèles acceptent `PLACE_TRAP` avec `coordinates` et exposent `board[y][x].trap`
(`placedBy`, `placedAtTurn`, `armedFromTurn`, `expiresAfterTurn`) et
`bot.data.nextTrapTurn` (10 initialement). La stratégie fournie reste un exemple :
elle n’exploite pas automatiquement les pièges.

Une pose remplace le piège présent sur la case. Posé au tour T, il est actif de
T+1 à T+10 inclus ; la recharge autorise une nouvelle pose à T+10. Après chaque
action, les pièges sous les deux bots infligent 1 PV puis disparaissent, y compris
sous leur créateur. Jouer deuxième peut donc empêcher de s’en échapper à temps.
La couleur du créateur est purement visuelle. Les pièges ne bloquent pas les tirs.
`currentTurn` compte les tours terminés : la prochaine action résout `currentTurn+1`.

La zone rouge est annoncée aux tours 70/80/90/100 et active aux tours 75/85/95/105,
du bord vers le centre : 1 PV par bot en zone active en fin de tour global.
