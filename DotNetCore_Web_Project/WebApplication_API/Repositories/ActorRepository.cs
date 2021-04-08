using HelperClassLibrary;
using ModelClassLibrary;
using System.Collections.Generic;
using System.Linq;
using WebApplication_API.DbContexts;
using WebApplication_API.SakilaModels;

//本例演示简单的synchronous
namespace WebApplication_API.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private SakilaContextMSSQL _dbContext;

        public ActorRepository(SakilaContextMSSQL sakila)
        {
            _dbContext = sakila;
        }

        public IList<ActorModel> Get()
        {
            return _dbContext.Actor.Select(a => a.Copy<Actor, ActorModel>()).ToList();
        }

        public ActorModel Get(int id)
        {
            var actor = _dbContext.Actor.SingleOrDefault(a => a.ActorId == id);
            return actor?.Copy<Actor, ActorModel>();
        }

        public string CreateActor(ActorModel actor)
        {
            try
            {
                var existActor = _dbContext.Actor.Where(_ => _.ActorId == actor.ActorID).FirstOrDefault();
                if (existActor != null)
                    return "Actor already exists";
                _dbContext.Actor.Add(actor.Copy<ActorModel, Actor>());
                _dbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
            return null;
        }

        public string UpdateActor(int id, ActorModel actor)
        {
            try
            {
                var existActor = _dbContext.Actor.SingleOrDefault(a => a.ActorId == id);
                if (existActor == null)
                    return "actor does not exists";
                _dbContext.Entry(existActor).CurrentValues.SetValues(actor);
                _dbContext.SaveChanges();
            }
            catch (System.Exception)
            {

                throw;
            }
            return null;
        }

        public string DeleteActorById(int id)
        {
            try
            {
                var existActor = _dbContext.Actor.SingleOrDefault(a => a.ActorId == id);
                if (existActor == null)
                    return "Ailm does not exists";
                _dbContext.Actor.Remove(existActor);
                _dbContext.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
            return null;
        }
    }
}
