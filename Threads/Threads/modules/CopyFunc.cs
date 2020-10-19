using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Threads.modules
{
    public class CopyFunc
    {
        public readonly string to;
        public readonly string from;

        public CopyFunc(string from, string to)
        {
            this.from = from;
            this.to = to;
        }

        public void Invoke()
        {
            try
            {
                File.Copy(from, to, true);
                Console.WriteLine("Copied file from " + from + " to " + to);
                CopyDirectory.CountOfDoneTasks++;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
}
}
