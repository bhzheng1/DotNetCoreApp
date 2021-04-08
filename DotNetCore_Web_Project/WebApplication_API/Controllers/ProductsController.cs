using Microsoft.AspNetCore.Mvc;
using WebApplication_API.DbContexts;
using System.Linq;
using ModelClassLibrary;

namespace WebApplication_API.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private FakeData _fakeData;
        public ProductsController(FakeData fakeData)
        {
            _fakeData = fakeData;
        }
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_fakeData.Products.Values.ToArray());
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (_fakeData.Products.ContainsKey(id))
            {
                return Ok(_fakeData.Products[id]);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid || product == null)
            {
                return BadRequest();
            }
            else
            {
                var maxExistingID = 0;
                if (_fakeData.Products.Count > 0)
                {
                    maxExistingID = _fakeData.Products.Keys.Max();
                }

                product.ID = maxExistingID + 1;
                _fakeData.Products.Add(product.ID, product);

                return Created($"api/products/{product.ID}", product);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            else if (_fakeData.Products.ContainsKey(id))
            {
                _fakeData.Products[id] = product;
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
            if (_fakeData.Products.ContainsKey(id))
            {
                _fakeData.Products.Remove(id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}