using template_csharp.Models;

namespace template_csharp.Services;

public interface IBotService
{
    BotInfoDTO GetBotInfoAsync();
    PlayResponseDTO PlayAsync(GameBotDTO gameBotDto);
}