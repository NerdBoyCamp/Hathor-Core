using System;

namespace Hathor
{
    class TestBattleRun
    {
        static void Main(string[] args)
        {
            // var abilityCreater = ;
            var charaterCreater = new TestCharacterCreater();
            var actionCreater = new DefaultAtionCreater();
            var stageCreater = new TestStageCreater();
            var stage = stageCreater.Create(
                "test", null, charaterCreater, null, null, actionCreater);
            if (stage == null)
            {
                Console.WriteLine("��������ʧ��");
            }

            while(true)
            {
                stage.Update();
            }
        }
    }
}
