using System;
using System.Collections.Generic;
using System.Threading;
using Hathor;

namespace Hathor
{
    class TestBattleRun
    {
        static void Main(string[] args)
        {
            // Display the number of command line arguments.
            // Console.WriteLine(args.Length);
            // var a = new Dictionary<string, int>();
            // a["a"] = 1;
            // a["a"] = 2;
            // var b = 0;
            // var c = 0;
            // Console.WriteLine(a["a"]);
            // Console.WriteLine(a.TryGetValue("a", out b));
            // Console.WriteLine(a.TryGetValue("b", out c));
            // Console.WriteLine(b);
            // Console.WriteLine(c);
            // Console.WriteLine(Util.RamdonID());
            Console.WriteLine(DateTime.UtcNow.Ticks / 10000000.0);
            Thread.Sleep(1000);
            Console.WriteLine(DateTime.UtcNow.Ticks / 10000000.0);
        }
    }
}
