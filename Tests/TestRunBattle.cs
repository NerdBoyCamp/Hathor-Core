using System;

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

            while(true)
            {
                stage.Update();
            }
        }
    }
}
