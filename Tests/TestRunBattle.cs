using System;
using System.Threading;

namespace Hathor
{
    class TestRunBattle
    {
        static public void Run(string[] args)
        {
            var buildingCreater = new TestBuildingCreater();
            var charaterCreater = new TestCharacterCreater();
            var actionCreater = new DefaultAtionCreater();
            var stageCreater = new TestStageCreater();
            var itemCreater = new TestItemCreater();
            var abilityCreater = new TestAbilityCreater();
            var stage = stageCreater.Create(
                "test",
                buildingCreater,
                charaterCreater,
                itemCreater,
                abilityCreater,
                actionCreater
            );
            if (stage == null)
            {
                Console.WriteLine("³¡¾°´´½¨Ê§°Ü");
            }

            float timeFrameStartPrev = DateTime.UtcNow.Ticks / 10000000.0f;
            float timeFrameDeltaStd = 1.0f / 60.0f;  // 60Ö¡

            while (true)
            {
                float timeFrameStart = DateTime.UtcNow.Ticks / 10000000.0f;
                float timeFrameDelta = timeFrameStart - timeFrameStartPrev;
                if (timeFrameDelta < timeFrameDeltaStd)
                {
                    Thread.Sleep((int)(timeFrameDeltaStd - timeFrameDelta) * 1000);
                }
                stage.Update(Math.Max(timeFrameDelta, timeFrameDeltaStd));
            }
        }
    }
}
