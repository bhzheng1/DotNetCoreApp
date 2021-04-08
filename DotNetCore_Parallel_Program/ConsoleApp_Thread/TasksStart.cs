using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//Writing I/O- and CPU-bound asynchronous code is straightforward using the .NET Task-based async model. 
namespace ConsoleApp_Thread
{
    class TasksStart
    {
        public static void Run() {

            //create a task
            Task t = new Task(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    //print the thread id 
                    var threadId = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("task thread id: " + threadId);
                }
            });

            //start task t execution
            t.Start();

            //Codes below will execute parallelly with task t  
            for (int i = 0; i <100; i++)
            {
                //print the main thread id
                var threadId = Thread.CurrentThread.ManagedThreadId;
                Console.WriteLine("run thread Id: " + threadId);
            }

            //task that returns a value
            Task<int> t1 = Task.Run<int>(() => { return 100; });

            //Will force the thread that is trying to read the result to wait until the task is finished
            Console.WriteLine(t1.Result);

            //wait for task t to complete its execution
            //the main thread pauses its execution until the task t completes its execution.
            t.Wait();

            Console.WriteLine("press enter to terminate the process!");
            Console.ReadLine();
        }
    }
}
