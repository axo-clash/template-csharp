using template_csharp.Models;

namespace template_csharp.Services;

public interface IBotService
{
    BotInfoDTO GetBotInfo();
    PlayResponseDTO Play(GameBotDTO gameBotDto);
}