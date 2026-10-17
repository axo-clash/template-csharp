namespace template_csharp.Models;

public class PlayResponseDTO
{
    public CoordDTO? Coordinates { get; set; }
    public ActionType Action { get; set; }
}

public enum ActionType
{
    MOVE_LEFT,
    MOVE_RIGHT,
    MOVE_UP,
    MOVE_DOWN,
    SHOOT_LEFT,
    SHOOT_RIGHT,
    SHOOT_UP,
    SHOOT_DOWN,
    PLACE_TRAP,
    UNKNOWN
}