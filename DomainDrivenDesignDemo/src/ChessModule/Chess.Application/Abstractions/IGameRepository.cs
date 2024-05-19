using Chess.Domain.Aggregates;

namespace Chess.Application.Abstractions;

public interface IGameRepository
{
    public Task<Game> GetGameById(int id);
}