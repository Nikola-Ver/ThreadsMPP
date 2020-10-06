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

        private bool flagWork = true;
        
        public TaskQueue(int quantityThreads)
        {
            arrayOfThreads = new Thread[quantityThreads];
            for (int i = 0; i < quantityThreads; i++)
            {
                arrayOfThreads[i] = new Thread(TakeTask);
                arrayOfThreads[i].Start();
            }
        }

        private void TakeTask()
        {
            while (flagWork)
            {
                if (queueTasks.TryDequeue(out var task))
                    task.Invoke();
                else
                    Thread.Sleep(300);
            }
        }

        public void EnqueueTask(TaskDelegate task)
        {
            queueTasks.Enqueue(task);
        }

        public void StopWork()
        {
            flagWork = false;
        }
    }
}
