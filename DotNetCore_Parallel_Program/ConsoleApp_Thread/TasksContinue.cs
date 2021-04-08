using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Thread
{
    class TasksContinue
    {
        public static void Run()
        {
            //add callback to task
            Task<int> t = Task.Run<int>(() =>
            {
                return 32;
            }).ContinueWith((i) =>
            {
                return i.Result * 3;
            });

            t.ContinueWith((i) => {
                Console.WriteLine(i.Result);
            });

            Task<int> t1 = Task.Run(() =>
            {
                return 32;
            });

            t1.ContinueWith((i) =>
            {
                Console.WriteLine("Canceled");
            }, TaskContinuationOptions.OnlyOnCanceled);
            t1.ContinueWith((i) =>
            {
                Console.WriteLine("Faulted");
            }, TaskContinuationOptions.OnlyOnFaulted);

            var completedTask = t1.ContinueWith((i) =>
            {
                Console.WriteLine(i.Result);
                Console.WriteLine("Completed");
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            Console.WriteLine("Press Enter to terminate!");
            Console.ReadLine();
        }
    }
}
