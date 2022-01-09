using ConsoleApp1.ReflectionSamples;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var now = DateTime.Now;
            var mins = Math.Floor(now.TimeOfDay.TotalMinutes);
            Console.WriteLine(mins);
            //MethodReflection.MethodPropertyReflection();
            Console.WriteLine("Hello World!");
        }
    }
}
