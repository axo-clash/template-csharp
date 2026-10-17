using template_csharp.Models;

namespace template_csharp.Services;

public class BotService : IBotService
{
    public BotInfoDTO GetBotInfo()
    {
        return new BotInfoDTO
        {
            Name = "CSharp Template Bot",
            Version = "1.0.0"
        };
    }

    public PlayResponseDTO Play(GameBotDTO gameBotDto)
    {
        // PLACE_TRAP needs a target and cooldown handling; this starter only moves and shoots.
        var random = new Random();
        var actions = Enum.GetValues<ActionType>()
            .Where(action => action != ActionType.PLACE_TRAP && action != ActionType.UNKNOWN)
            .ToArray();
        var randomAction = actions[random.Next(actions.Length)];

        return new PlayResponseDTO
        {
            Action = randomAction
        };
    }
}