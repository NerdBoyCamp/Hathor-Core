using System;
using System.Runtime;
using System.Collections.Generic;
using System.Threading;
using Newtonsoft.Json;
using Hathor;

namespace Hathor
{
    class Test
    {
        public string Name { set; get; }
    }

    class TestBattleRun
    {
        static void Main(string[] args)
        {
            
            // var stageCreater = TestStageCreater().Create();
            string configs = "{\"Name\": \"eheh\"}";

            Test configObj = JsonConvert.DeserializeObject<Test>(configs);
            
            // var configObjects = JsonValue.Parse(configs);

            // Console.WriteLine(DateTime.UtcNow.Ticks / 10000000.0);
            // object a = new DefaultAtionCreater();
            // var prop = a.GetType().GetProperty("test");


            // Console.WriteLine(prop == null);
            // a.TryGetValue("A", out aa);
            // Console.WriteLine(Util.RamdonID());
            // Thread.Sleep(1000);
            // Console.WriteLine(DateTime.UtcNow.Ticks / 10000000.0);
        }
    }
}
