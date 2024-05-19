using Chess.Domain.Enums;
using Chess.Domain.ValueObjects;
using SharedKernel;

namespace Chess.Domain.AggregateRoots;

public class Game : AggregateRoot<int>
{
    public Game(int id, int whitePlayerId, int blackPlayerId) : base(id)
    {
        WhitePlayerId = whitePlayerId;
        BlackPlayerId = blackPlayerId;
    }

    public int WhitePlayerId { get; }
    public int BlackPlayerId { get; }
    public Result Result { get; private set; }

    public void EndGame(GameResult result, GameResultReason reason)
    {
        Result = new Result(result, reason);
    }
}