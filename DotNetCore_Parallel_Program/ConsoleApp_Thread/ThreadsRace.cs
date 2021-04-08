using System;
using System.Threading;

//当多Thread访问或修改公共资源时容易产生竞争问题， 导致对公共资源的修改不确定
// the thread scheduling algorithm can swap between threads at any time.
//可以使用lock解决这类问题
namespace ConsoleApp_Thread
{
    class ThreadsRace
    {
        private static int sum;
        public static void Run() {
            Thread t1 = new Thread(() => {
                for (int i = 0; i < 1000000; i++)
                {
                    //increment sum value
                    sum++;
                }
            });

            Thread t2 = new Thread(() => {
                for (int i = 0; i < 1000000; i++)
                {
                    //increment sum value
                    sum++;
                }
            });

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.WriteLine("sum: " + sum);

            Console.WriteLine("Press enter to terminate!");
            Console.ReadLine();
        }
    }
}
