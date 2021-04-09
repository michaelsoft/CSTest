using System.Threading;
using System;
using System.Text;

namespace CSTest
{
    class MemoryUsageTest
    {
        public static void Test()
        {
            for (var i=0; i < 100; i++)
            {
                Console.WriteLine($">> {i}");
                var data = new string('*', 10 * 1024 );
                Thread.Sleep(1000);
            }
        }
    }
}