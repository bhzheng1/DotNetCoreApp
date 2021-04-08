using Newtonsoft.Json;
using WebApplication_API.DbContexts;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;
using ModelClassLibrary;

namespace XUnitTestProject
{
    public class Product_EndToEnd_Test
    {
        private FakeData _fakeData = new FakeData();

        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:50859");
            var acceptType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(acceptType);
            return httpClient;
        }

        private bool SameProduct(Product p1, Product p2)
        {
            return p1.ID == p2.ID && p1.Name == p2.Name && p1.Price == p2.Price;
        }

        [Fact]
        public async void GetActionTest()
        {
            var httpClient = GetHttpClient();
            var allProductsResponse = await httpClient.GetAsync("api/productsTest");
            Assert.True(allProductsResponse.IsSuccessStatusCode);
            var allProductsJson = await allProductsResponse.Content.ReadAsStringAsync();
            var allProducts = JsonConvert.DeserializeObject<Product[]>(allProductsJson);
            Assert.NotNull(allProducts);
            Assert.True(allProducts.Length > 0);
        }

        [Fact]
        public async void PostActionTest()
        {
            int oldCount = _fakeData.Products.Count;
            int oldMaxID = _fakeData.Products.Keys.Max();

            var httpClient = GetHttpClient();
            var product = new Product { Name = "Test Product", Price = 9.9 };
            var productJson = JsonConvert.SerializeObject(product);
            var httpContent = new StringContent(productJson, Encoding.UTF8, "application/json");
            var newProductReponse = await httpClient.PostAsync("api/productsTest", httpContent);
            Assert.True(newProductReponse.IsSuccessStatusCode);
            var newProductJson = await newProductReponse.Content.ReadAsStringAsync();
            var newProduct = JsonConvert.DeserializeObject<Product>(newProductJson);
            product.ID = oldMaxID + 1;
            Assert.True(SameProduct(newProduct, product));
        }

        [Fact]
        public async void PutActionTest()
        {
            var httpClient = GetHttpClient();
            var product = _fakeData.Products[0];
            var productJson = JsonConvert.SerializeObject(product);
            var httpContent = new StringContent(productJson, Encoding.UTF8, "application/json");
            var putResponse = await httpClient.PutAsync("api/productsTest/0", httpContent);
            Assert.True(putResponse.IsSuccessStatusCode);
        }

        // [Fact]
        // public async void DeleteActionTest() {
        //     var httpClient = GetHttpClient();
        //     var deleteResponse = await httpClient.DeleteAsync("api/products/4");
        //     Assert.True(deleteResponse.IsSuccessStatusCode);
        //     deleteResponse = await httpClient.DeleteAsync("api/products/101");
        //     Assert.False(deleteResponse.IsSuccessStatusCode);
        // }
    }
}
