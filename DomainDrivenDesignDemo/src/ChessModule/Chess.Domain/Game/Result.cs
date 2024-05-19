using Chess.Domain.Enums;

namespace Chess.Domain.ValueObjects;

public record Result(GameResult GameResult, GameResultReason ResultReason);