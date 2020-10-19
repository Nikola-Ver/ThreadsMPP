using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Threads.modules
{
    class CopyDirectory
    {
        public static int CountOfTasks = 1;
        public static int CountOfDoneTasks = 0;
        private static List<TaskQueue.TaskDelegate> tasks = new List<TaskQueue.TaskDelegate>();

        public static TaskQueue.TaskDelegate[] Copy(string sourceDirectory, string targetDirectory)
        {
            var diSource = new DirectoryInfo(sourceDirectory);
            var diTarget = new DirectoryInfo(targetDirectory);

            if (diSource.Exists)
            {
                Directory.CreateDirectory(targetDirectory);
                CopyAll(diSource, diTarget);
                return tasks.ToArray();
            }
            return null;
        }
        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            var files = source.GetFiles();
            CountOfTasks += files.Length;

            for (int i = 0; i < files.Length; i++)
                tasks.Add(new CopyFunc(Path.Combine(source.FullName, files[i].Name), Path.Combine(target.FullName, files[i].Name)).Invoke);

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
