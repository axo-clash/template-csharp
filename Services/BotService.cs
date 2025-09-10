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
        // Return random action from all ActionType enum values
        var random = new Random();
        var actions = Enum.GetValues(typeof(ActionType)).Cast<ActionType>();
        var randomAction = actions.ElementAt(random.Next(actions.Count()));

        return new PlayResponseDTO
        {
            Action = randomAction
        };
    }
}