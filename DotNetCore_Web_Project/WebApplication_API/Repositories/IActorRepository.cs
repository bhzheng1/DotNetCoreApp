using ModelClassLibrary;
using System.Collections.Generic;

namespace WebApplication_API.Repositories
{
    public interface IActorRepository
    {
        string CreateActor(ActorModel actor);
        string DeleteActorById(int id);
        IList<ActorModel> Get();
        ActorModel Get(int id);
        string UpdateActor(int id, ActorModel actor);
    }
}