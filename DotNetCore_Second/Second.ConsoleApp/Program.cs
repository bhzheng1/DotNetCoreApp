using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Second.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(string.Join(",", null));
            Console.WriteLine("Hello World!");

            var tests = new List<Test>()
            {
                new Test{FirstName="hello1",Age=4 },
                new Test{FirstName="hello2",Age=4 },
                new Test{FirstName="hello3",Age=4 },
                new Test{FirstName="hello1", Age=2 },
                new Test{FirstName="hello2",Age=2 },
                new Test{FirstName="hello3",Age=2 },
            };
            foreach (var t in tests) {
                Console.WriteLine($"{t.FirstName} {t.Age}");
            }

            tests = tests.OrderByDescending(y => y.Age == 2).ThenByDescending(y => y.Age == 4).ToList();
            foreach (var t in tests)
            {
                Console.WriteLine($"{t.FirstName} {t.Age}");
            }
        }

        static void Test1()
        {
            var t = new Test();
            var ty = t.GetType();
            foreach (FieldInfo p in typeof(Test).GetFields())
            {
                string propertyName = p.Name;
                Console.WriteLine(propertyName);
            }

            if (ty.GetField("FirstName") != null)
            {
                Console.WriteLine(ty.GetField("FirstName").Name);
            }
        }
    }
    public class Test
    {
        public string FirstName;
        public string LastName;
        public int Age;
        public string Address;
    }
}
