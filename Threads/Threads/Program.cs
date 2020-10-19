using System;
using Threads.modules;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter the number of threads: ");
            var countOfThreads = Console.ReadLine();
            Console.Write("The file from where we copy: "); 
             var from = Console.ReadLine();
            Console.Write("The file where we copy: ");
            var to = Console.ReadLine();

            try
            {
                var taskQueue = new TaskQueue(int.Parse(countOfThreads));
                var copyTasks = CopyDirectory.Copy(from, to);

                CopyDirectory.countOfTasks--;

                if (copyTasks != null)
                {
                    foreach (var task in copyTasks)
                    {
                        taskQueue.EnqueueTask(task);
                    }
                }

                while (CopyDirectory.countOfDoneTasks != CopyDirectory.countOfTasks) { }
                Console.WriteLine("da");
            }
            catch(Exception e)
            {
                Console.WriteLine("\n\n" + e.Message);
            };
        }
    }
}
