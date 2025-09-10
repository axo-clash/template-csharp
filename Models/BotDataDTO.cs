namespace template_csharp.Models;

public class BotDataDTO
{
    public int Life { get; set; }
    public int ForbiddenActions { get; set; }
    public BotState State { get; set; }
    public long LastActionTimeMs { get; set; }
}

public enum BotState
{
    UNKNOWN,
    READY,
    PLAYING,
    WAITING_FOR_OPPONENT
}