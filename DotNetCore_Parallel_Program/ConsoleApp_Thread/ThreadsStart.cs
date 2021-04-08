using System;
using System.Threading;

// thread主要用于多任务同时进行
// 本例演示了它的基本用法
namespace ConsoleApp_Thread
{
    class ThreadsStart
    {
        public static void Run()
        {
            Thread.CurrentThread.Name = "Run";
            //initialize a thread class object 
            //And pass your custom method name to the constructor parameter
            Thread t = new Thread(SomeMethod);

            var t2 = new Thread(Speak);

            t.Name = "Parallel Thread";
            t.Priority = ThreadPriority.BelowNormal;

            //If you use a thread to monitor an activity, such as a socket connection, set its IsBackground property to true 
            //so that the thread does not prevent your process from terminating.
            t.IsBackground = false;

            //start running your thread
            t.Start();

            //pass parameter for the speak method in thread's start method below
            t2.Start("Hello there");

            //while thread is running in parallel you can carry out other operations here
            for (int i = 1; i <= 10; i = i + 2)
            {
                Console.WriteLine(Thread.CurrentThread.Name + "--" + i);
            }

            //wait until Thread "t" is done with its execution.
            t.Join();
            t2.Join();

            Console.WriteLine("Press Enter to terminate!");
            //Console.ReadLine();
        }
        private static void SomeMethod()
        {
            //your code here that you want to run parallel
            //most of the time it will be a CPU bound operation
            for (int i = 0; i <= 100; i = i + 2)
            {
                Console.WriteLine(Thread.CurrentThread.Name + "--" + i);
            }

            Console.WriteLine("Hello World!");
        }

        private static void Speak(object s)
        {
            //your code here that you want to run parallel
            //most of the time it will be a CPU bound operation

            string say = s as string;
            Console.WriteLine(say);
        }
    }
}
