using System;

namespace Hathor
{
    class TestStageCreater : IStageCreater
    {
        protected ICharacter CreateCharacter(
            ICharacterCreater characterCreater,
            IItemCreater itemCreater,
            IAbilityCreater abilityCreater
        )
        {
            while (true)
            {
                var c = characterCreater.Create(
                    Console.ReadLine(), itemCreater, abilityCreater);
                if (c != null)
                {
                    return c;
                }
                else
                {
                    Console.WriteLine("无效角色，请重新选择");
                }
            }
        }

        // 创建场景
        public IStage Create(
            string templateId,
            IBuildingCreater buildingCreater,
            ICharacterCreater characterCreater,
            IItemCreater itemCreater,
            IAbilityCreater abilityCreater,
            IActionCreater actionCreater
        )
        {
            Console.WriteLine("请选择角色1：");
            var char1 = this.CreateCharacter(characterCreater, itemCreater, abilityCreater);

            Console.WriteLine("请选择角色2：");
            var char2 = this.CreateCharacter(characterCreater, itemCreater, abilityCreater);

            var battle1 = char1.GetBattle();
            var battle2 = char2.GetBattle();

            if (battle1 == null || battle2 == null)
            {
                return null;
            }

            if (battle1.Dexterity.Value < battle2.Dexterity.Value)
            {
                // 敏捷高的排前面
                var charTmp = char1;
                char1 = char2;
                char2 = charTmp;
            }

            return new TestBattleStage(actionCreater, new ICharacter[2] { char1, char2 });
        }
    }
}