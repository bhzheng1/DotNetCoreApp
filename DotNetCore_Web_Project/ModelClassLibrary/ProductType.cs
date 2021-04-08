using System.Collections.Generic;

namespace ModelClassLibrary
{
    public class ProductType
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public IList<Product> Products { get; } = new List<Product>();
        public bool CanBeRemoved { get => Products.Count == 0; }
    }
}
