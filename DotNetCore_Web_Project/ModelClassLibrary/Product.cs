namespace ModelClassLibrary
{
    public class Product
    {
        public Product()
        {

        }
        public Product(int id, string name, double price)
        {
            this.ID = id;
            this.Name = name;
            this.Price = price;
        }
        public int ID { get; set; }
        public int? TypeID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public ProductType Type { get; set; }
    }
}
