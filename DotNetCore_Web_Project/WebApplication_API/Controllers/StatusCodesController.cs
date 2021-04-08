using Microsoft.AspNetCore.Mvc;
using WebApplication_API.DbContexts;
using System.Linq;
using ModelClassLibrary;

namespace WebApplication_API.Controllers
{

    [Route("api/[controller]")]
    public class StatusCodesController : Controller
    {
        private FakeData _fakeData;
        public StatusCodesController(FakeData fakeData)
        {
            _fakeData = fakeData;
        }
        [HttpGet]
        public ActionResult Get()
        {
            if (_fakeData.Products != null)
            {
                return Ok(_fakeData.Products.Values.ToArray());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            if (_fakeData.Products.ContainsKey(id))
                return Ok(_fakeData.Products[id]);
            else
                return NotFound();
        }

        [HttpGet("from/{low}/to/{high}")]
        public ActionResult Get(int low, int high)
        {
            var products = _fakeData.Products.Values
            .Where(p => p.Price >= low && p.Price <= high).ToArray();
            if (products.Length > 0)
            { // LINQ guarantees the products won't be null
                return Ok(products);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Product product)
        {
            product.ID = _fakeData.Products.Keys.Max() + 1;
            _fakeData.Products.Add(product.ID, product);
            return Created($"api/products/{product.ID}", product); // contains the new ID
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Product product)
        {
            if (_fakeData.Products.ContainsKey(id))
            {
                var target = _fakeData.Products[id];
                target.ID = product.ID;
                target.Name = product.Name;
                target.Price = product.Price;
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