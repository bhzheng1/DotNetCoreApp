using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication_API.DbContexts;
using WebApplication_API.SakilaModels;

//本例示范routing and parameter binding
namespace WebApplication_API.Controllers
{
    [Route("api/[controller]")]
    public class ActorController : Controller
    {

        private SakilaContextMSSQL _dbContext;

        public ActorController(SakilaContextMSSQL sakila)
        {
            _dbContext = sakila;
        }

        // GET api/actors
        [HttpGet]
        public ActionResult<IList<Actor>> Get()
        {
            return Ok(_dbContext.Actor.ToArray());
        }

        // GET api/actors/101
        [HttpGet("{id}")]
        public ActionResult<Actor> Get(int id)
        {
            var actor = _dbContext.Actor.SingleOrDefault(a => a.ActorId == id);
            if (actor != null)
            {
                return Ok(actor);
            }
            else
            {
                return NotFound();
            }
        }

        // POST api/actors
        [HttpPost]
        public IActionResult Post([FromBody] Actor actor)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _dbContext.Actor.Add(actor);
            _dbContext.SaveChanges();
            return Created("api/actors", actor);
        }

        // PUT api/actors/101
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Actor actor)
        {
            var target = _dbContext.Actor.SingleOrDefault(a => a.ActorId == id);
            if (target != null && ModelState.IsValid)
            {
                _dbContext.Entry(target).CurrentValues.SetValues(actor);
                _dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/actors/101
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var actor = _dbContext.Actor.SingleOrDefault(a => a.ActorId == id);
            if (actor != null)
            {
                _dbContext.Actor.Remove(actor);
                _dbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}