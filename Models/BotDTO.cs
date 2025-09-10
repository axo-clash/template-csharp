namespace template_csharp.Models;

public class BotDTO
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Version { get; set; }
    public string? Url { get; set; }
    public BotDataDTO? Data { get; set; }
    public BotType BotType { get; set; }
    public CoordDTO? Coordinates { get; set; }
    public bool Faster { get; set; }
}

public enum BotType
{
    FIRST_BOT,
    SECOND_BOT
}