using Microsoft.AspNetCore.Mvc;
using WebApplication_API.Controllers;
using WebApplication_API.DbContexts;
using System.Linq;
using Xunit;
using ModelClassLibrary;

namespace XUnitTestProject
{
    public class Product_Function_Test
    {
        private FakeData _fakeData = new FakeData();
        [Fact]
        public void CreateProductsControllerInstanceTest()
        {
            var controller = new ProductsController(_fakeData);
            Assert.NotNull(controller);
        }

        [Fact]
        public void RepositoryInitializtionTest()
        {
            Assert.NotNull(_fakeData.Products);
            Assert.Equal(10, _fakeData.Products.Count);

            foreach (var id in new int[] { 0, 1, 2, 3 })
            {
                Assert.True(_fakeData.Products.ContainsKey(id));
            }

            foreach (var key in _fakeData.Products.Keys)
            {
                Assert.Equal(_fakeData.Products[key].ID, key);
            }
        }

        [Fact]
        public void GetActionTest()
        {
            var controller = new ProductsController(_fakeData);
            Assert.IsType<OkObjectResult>(controller.Get());
            foreach (var key in _fakeData.Products.Keys)
            {
                Assert.IsType<OkObjectResult>(controller.Get(key));
            }
        }

        [Fact]
        public void PostActionTest()
        {
            var controller = new ProductsController(_fakeData);
            int oldCount = _fakeData.Products.Count;
            var product = new Product { Name = "Test Product", Price = 9.9 };
            Assert.IsType<CreatedResult>(controller.Post(product));
            Assert.Equal(_fakeData.Products.Count, oldCount + 1);
        }

        [Fact]
        public void DeleteActionTest()
        {
            var controller = new ProductsController(_fakeData);
            int oldCount = _fakeData.Products.Count;
            var maxKey = _fakeData.Products.Keys.Max();
            Assert.IsType<OkResult>(controller.Delete(maxKey));
            Assert.Equal(_fakeData.Products.Count, oldCount - 1);
        }

        [Fact]
        public void PutActionTest()
        {
            var controller = new ProductsController(_fakeData);
            int oldCount = _fakeData.Products.Count;
            var maxKey = _fakeData.Products.Keys.Max();
            var product = _fakeData.Products[maxKey];
            product.Name = "Changed";
            Assert.IsType<OkResult>(controller.Put(maxKey, product));
            Assert.Equal(_fakeData.Products.Count, oldCount);
        }
    }
}
