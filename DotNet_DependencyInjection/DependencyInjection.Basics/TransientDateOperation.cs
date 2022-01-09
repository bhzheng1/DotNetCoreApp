using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DependencyInjection.Basics
{
    public class TransientDateOperation
    {
        public TransientDateOperation()
        {
            Console.WriteLine("Transient service is created!");
        }
    }
}
