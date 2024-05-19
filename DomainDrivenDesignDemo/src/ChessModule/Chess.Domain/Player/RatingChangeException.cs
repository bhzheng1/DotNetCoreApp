namespace Chess.Domain.Exceptions;

public class RatingChangeException : Exception
{
    public RatingChangeException()
    {
    }

    public RatingChangeException(string message)
        : base(message)
    {
    }

    public RatingChangeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}