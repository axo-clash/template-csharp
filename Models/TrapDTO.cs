namespace template_csharp.Models;

public class TrapDTO
{
    public BotType PlacedBy { get; set; }
    public int PlacedAtTurn { get; set; }
    public int ArmedFromTurn { get; set; }
    public int ExpiresAfterTurn { get; set; }
}
