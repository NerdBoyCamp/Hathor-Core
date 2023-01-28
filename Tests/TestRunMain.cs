// using System;
// using System.Collections;
// using Newtonsoft.Json.Linq;

namespace Hathor
{
    class TestRunMain
    {
        static void Main(string[] args)
        {
            // TestRunUtil.Run(args);
            TestRunBattle.Run(args);

            // dynamic a = new { Name = "aaa" };
            // string a = "{\"Name\":[1, 2]}";
            // dynamic b = JObject.Parse(a);
            // IEnumerable enumerable = (b.Name1 as IEnumerable);
            // foreach (var n in enumerable)
            // {
            // Console.WriteLine(n);
            // }
        }
    }
}
