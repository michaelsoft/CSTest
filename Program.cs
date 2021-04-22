using System.Reflection.Emit;
using System.Threading.Tasks.Dataflow;
using System.Net.Sockets;
using System.Threading.Tasks.Sources;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Reflection.PortableExecutable;
using System.IO.Enumeration;
using System.Security.AccessControl;
using System.Data;
using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace CSTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RoslynTest.Test();
            Console.ReadLine();
        }

        static async Task Main2(string[] args)
        {
            var scheduler = new BatchRuleScheduler();
            await scheduler.RunAsync();
            Console.Read();
        }

        async static Task<int> TestAsync()
        {
            return await Task.FromResult(0);
        }

        

    }
}
