using Microsoft.AspNetCore.Mvc;
using template_csharp.Models;
using template_csharp.Services;

namespace template_csharp.Controllers;

[ApiController]
[Tags("Bot")]
public class BotController : ControllerBase
{
    private readonly IBotService _botService;
    private readonly ILogger<BotController> _logger;

    public BotController(IBotService botService, ILogger<BotController> logger)
    {
        _botService = botService;
        _logger = logger;
    }

    [HttpGet("info")]
    [ProducesResponseType(typeof(BotInfoDTO), StatusCodes.Status200OK)]
    public ActionResult<BotInfoDTO> GetBotInfo()
    {
        var botInfo = _botService.GetBotInfo();
        return Ok(botInfo);
    }

    [HttpPost("play")]
    [ProducesResponseType(typeof(PlayResponseDTO), StatusCodes.Status200OK)]
    public ActionResult<PlayResponseDTO> Play([FromBody] GameBotDTO gameBotDto)
    {
        var playResponse = _botService.Play(gameBotDto);
        return Ok(playResponse);
    }
}
