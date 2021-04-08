using System;
using System.Collections.Generic;
using System.Reflection;
using ModelClassLibrary;

namespace WebApplication_API.DbContexts
{
    public class FakeData
    {
        public IDictionary<int, Rocket> Rockets { get; private set; }
        public IDictionary<int, Product> Products { get; private set; }
        public FakeData()
        {
            Rockets = new Dictionary<int, Rocket>();
            Rockets.Add(0, new Rocket { ID = 0, Builder = "NASA", Target = "Moon", Speed = 7.8 });
            Rockets.Add(1, new Rocket { ID = 1, Builder = "NASA", Target = "Mars", Speed = 10.9 });
            Rockets.Add(2, new Rocket { ID = 2, Builder = "NASA", Target = "Jupiter", Speed = 42.1 });
            Rockets.Add(3, new Rocket { ID = 3, Builder = "NASA", Target = "Saturn", Speed = 0.0 });

            Products = new Dictionary<int, Product>();
            Products.Add(0, new Product { ID = 0, Name = "Apple", Price = 5.55 });
            Products.Add(1, new Product { ID = 1, Name = "Bike", Price = 6.66 });
            Products.Add(2, new Product { ID = 2, Name = "Coffee", Price = 7.77 });
            Products.Add(3, new Product { ID = 3, Name = "Duck", Price = 8.88 });
            Products.Add(4, new Product { ID = 4, Name = "Earphone", Price = 9.99 });
            Products.Add(5, new Product { ID = 5, Name = "Freezer", Price = 10.10 });
            Products.Add(6, new Product { ID = 6, Name = "Guitar", Price = 11.11 });
            Products.Add(7, new Product { ID = 7, Name = "Hook", Price = 12.12 });
            Products.Add(8, new Product { ID = 8, Name = "Ice Cream", Price = 14.14 });
            Products.Add(9, new Product { ID = 9, Name = "Jawbreaker", Price = 15.15 });
            Products.Add(10, new Product { ID = 10, Name = "Knife", Price = 16.16 });
            Products.Add(11, new Product { ID = 11, Name = "Lighter", Price = 17.17 });
            Products.Add(12, new Product { ID = 12, Name = "Mug", Price = 18.18 });
        }

        internal IDictionary<int, T> Set<T>()
        {
            Type t = MethodBase.GetCurrentMethod().DeclaringType;
            var o = Activator.CreateInstance(t);
            PropertyInfo[] properties = t.GetProperties();
            foreach (var item in properties)
            {
                if (item.PropertyType.GetGenericTypeDefinition() == typeof(IDictionary<,>))
                {
                    var valueType = item.PropertyType.GetGenericArguments()[1];
                    if (valueType == typeof(T))
                    {
                        object value = item.GetValue(o, null);
                        return (IDictionary<int, T>)value;
                    }

                }
            }
            return null;
        }
    }
}