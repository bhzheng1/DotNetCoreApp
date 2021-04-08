using Microsoft.AspNetCore.Mvc;
using WebApplication_API.DbContexts;
using System.Linq;
using ModelClassLibrary;

namespace WebApplication_API.Controllers
{
    [Route("api/[controller]")]
    public class RocketsController : Controller
    {
        private FakeData _fakeData;
        public RocketsController(FakeData fakeData)
        {
            _fakeData = fakeData;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_fakeData.Rockets.Values.ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (_fakeData.Rockets.ContainsKey(id))
            {
                return Ok(_fakeData.Rockets[id]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Rocket rocket)
        {
            if (!ModelState.IsValid || rocket == null)
            {
                return BadRequest();
            }
            else
            {
                var maxExistingID = 0;
                if (_fakeData.Rockets.Count > 0)
                {
                    maxExistingID = _fakeData.Rockets.Keys.Max();
                }

                rocket.ID = maxExistingID + 1;
                _fakeData.Rockets.Add(rocket.ID, rocket);

                return Created($"api/rockets/{rocket.ID}", rocket);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Rocket rocket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else if (_fakeData.Rockets.ContainsKey(id))
            {
                _fakeData.Rockets[id] = rocket;
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (_fakeData.Rockets.ContainsKey(id))
            {
                _fakeData.Rockets.Remove(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}