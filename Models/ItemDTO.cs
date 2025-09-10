namespace template_csharp.Models;

public class ItemDTO
{
    public ItemType Type { get; set; }
    public BotDTO? Owner { get; set; }
}

public enum ItemType
{
    BULLET_LEFT,
    BULLET_RIGHT,
    BULLET_UP,
    BULLET_DOWN
}