using Microsoft.AspNetCore.Mvc;
using template_csharp.Models;
using template_csharp.Services;

namespace template_csharp.Controllers;

[ApiController]
[Tags("Bot")]
public class BotController : ControllerBase
{
    private readonly IBotService _botService;

    public BotController(IBotService botService)
    {
        _botService = botService;
    }

    [HttpGet("info")]
    [ProducesResponseType(typeof(BotInfoDTO), StatusCodes.Status200OK)]
    public ActionResult<BotInfoDTO> GetBotInfo()
    {
        var botInfo = _botService.GetBotInfoAsync();
        return Ok(botInfo);
    }

    [HttpPost("play")]
    [ProducesResponseType(typeof(PlayResponseDTO), StatusCodes.Status200OK)]
    public ActionResult<PlayResponseDTO> Play([FromBody] GameBotDTO gameBotDto)
    {
        var response = _botService.PlayAsync(gameBotDto);
        return Ok(response);
    }
}