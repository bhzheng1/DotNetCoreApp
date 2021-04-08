using Microsoft.AspNetCore.Mvc;
using WebApplication_API.DbContexts;
using System.Linq;
using ModelClassLibrary;

//可以修改
namespace WebApplication_API.Controllers
{
    [Route("api/[controller]")] //[Route("api/inventories")]
    public class CustomRouteController : Controller
    {
        private FakeData _fakeData;
        public CustomRouteController(FakeData fakeData)
        {
            _fakeData = fakeData;
        }
        [HttpGet] //[HttpGet("all")] //[HttpGet, Route("all")]
        public Product[] Get()
        {
            return _fakeData.Products.Values.ToArray();
        }

        [HttpGet("{id}")] //[HttpGet("ByID/{id}")]
        public Product Get(int id)
        {
            if (_fakeData.Products.ContainsKey(id))
                return _fakeData.Products[id];
            else
                return null;
        }

        // [HttpGet("from/{low}/to/{high}")]
        // public Product[] Get(int low, int high) {
        //     var products = _fakeData.Products.Values
        //     .Where(p => p.Price >= low && p.Price <= high).ToArray();
        //     return products;
        // }

        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            product.ID = _fakeData.Products.Keys.Max() + 1;
            _fakeData.Products.Add(product.ID, product);
            return product; // contains the new ID
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
            if (_fakeData.Products.ContainsKey(id))
            {
                var target = _fakeData.Products[id];
                target.ID = product.ID;
                target.Name = product.Name;
                target.Price = product.Price;
            }
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (_fakeData.Products.ContainsKey(id))
            {
                _fakeData.Products.Remove(id);
            }
        }
    }
}