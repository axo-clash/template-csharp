namespace template_csharp.Models;

public class GameDTO
{
    public long Id { get; set; }
    public BotDTO? FirstBot { get; set; }
    public BotDTO? SecondBot { get; set; }
    public CaseDTO[][] Board { get; set; } = Array.Empty<CaseDTO[]>();
    public int CurrentTurn { get; set; }
    public List<ActionDTO> Actions { get; set; } = new();
    public Winner? Winner { get; set; }
    public EndReason? EndReason { get; set; }
    public FailureReason? FailureReason { get; set; }
    public GameStatus Status { get; set; }
}

public enum Winner
{
    FIRST_BOT,
    SECOND_BOT,
    DRAW
}

public enum EndReason
{
    FIRST_BOT_LIFE,
    SECOND_BOT_LIFE,
    FIRST_BOT_FORBIDDEN_ACTION,
    SECOND_BOT_FORBIDDEN_ACTION,
    GAME_TURN
}

public enum FailureReason
{
    BOT_CONNECTION_ERROR,
    BOT_INFO_RETRIEVAL_ERROR,
    GAME_EXECUTION_ERROR,
    INVALID_BOARD_STATE,
    BOT_COMMUNICATION_TIMEOUT,
    JSON_PROCESSING_ERROR,
    UNKNOWN_ERROR
}

public enum GameStatus
{
    STARTING,
    RUNNING,
    PAUSED,
    FINISHED,
    FAILED
}