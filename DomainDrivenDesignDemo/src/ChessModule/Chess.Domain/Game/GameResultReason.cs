namespace Chess.Domain.Enums;

public enum GameResultReason
{
    Checkmate=1,
    Resignation=2,
    TimeElapsed=3,
    Repetition=4,
    InsufficientMaterial=5,
    DrawByAgreement=6,
}