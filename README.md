# C# Bot Template for Axo Clash

This is a C# ASP.NET Core template for creating bots that compete in the Axo Clash game competition.

## Quick Start

### Prerequisites
- .NET 9.0 SDK

### Running the Bot

1. **Start the bot server:**
   ```bash
   dotnet run
   ```
   
   The bot will start on `http://localhost:5144`

2. **Verify it's working:**
   - GET `http://localhost:5144/info` - Returns bot information
   - POST `http://localhost:5144/play` - Accepts game state and returns bot action

## Implementing Your Bot Strategy

To customize your bot's behavior, modify the `PlayAsync` method in **`Services/BotService.cs`**:

```csharp
public PlayResponseDTO PlayAsync(GameBotDTO gameBotDto)
{
    // TODO: Implement your bot strategy here
    // Access game state: gameBotDto.Game
    // Access your bot data: gameBotDto.Bot
    
    // Current implementation: random action
    var random = new Random();
    var actions = Enum.GetValues(typeof(ActionType)).Cast<ActionType>();
    var randomAction = actions.ElementAt(random.Next(actions.Count()));

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
├── Controllers/
│   └── BotController.cs     # REST API endpoints
├── Services/
│   ├── IBotService.cs       # Service interface
│   └── BotService.cs        # 🎯 MODIFY THIS FILE
├── Models/                  # Data transfer objects
├── Program.cs              # Application entry point
└── Properties/
    └── launchSettings.json # Development configuration
```

## Development

- **Hot reload**: `dotnet watch run`
- **Build**: `dotnet build`
- **Clean**: `dotnet clean`

## Bot Information

Update your bot's name and version in `BotService.GetBotInfoAsync()`:

```csharp
return new BotInfoDTO
{
    Name = "Your Bot Name",
    Version = "1.0.0"
};
```