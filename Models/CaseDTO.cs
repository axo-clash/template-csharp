namespace template_csharp.Models;

public class CaseDTO
{
    public CoordDTO? Coordinates { get; set; }
    public CaseContent Content { get; set; }
    public List<ItemDTO> Items { get; set; } = new();
}

public enum CaseContent
{
    FIRST_BOT,
    SECOND_BOT,
    BULLET_LEFT,
    BULLET_RIGHT,
    BULLET_UP,
    BULLET_DOWN
}