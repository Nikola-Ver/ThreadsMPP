using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Threads.modules
{
    class TaskQueue
    {
        public delegate void TaskDelegate();
        private ConcurrentQueue<TaskDelegate> queueTasks = new ConcurrentQueue<TaskDelegate>();
        private Thread[] arrayOfThreads;

        public TaskQueue(int quantityThreads)
        {
            if (quantityThreads <= 0) throw new Exception("Wrong number of threads");

            arrayOfThreads = new Thread[quantityThreads];
            for (int i = 0; i < quantityThreads; i++)
            {
                arrayOfThreads[i] = new Thread(TakeTask);
                arrayOfThreads[i].Start();
            }
        }

        private void TakeTask()
        {
            while (CopyDirectory.countOfTasks != CopyDirectory.countOfDoneTasks)
            {
                if (queueTasks.TryDequeue(out var task))
                    task.Invoke();
                else
                    Thread.Sleep(100);
            }
        }

        public void EnqueueTask(TaskDelegate task)
        {
            queueTasks.Enqueue(task);
        }
    }
}
