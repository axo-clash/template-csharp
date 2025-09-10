namespace template_csharp.Models;

public class ActionDTO
{
    public long Id { get; set; }
    public long BotId { get; set; }
    public ActionType Action { get; set; }
    public bool Forbidden { get; set; }
    public long ExecutionTimeMs { get; set; }
    public DateTime CreatedDate { get; set; }
}