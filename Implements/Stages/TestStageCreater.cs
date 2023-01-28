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
                ICharacter c = characterCreater.Create(
                    Console.ReadLine(),
                    itemCreater,
                    abilityCreater
                );
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

        protected IItem CreateItem(
            IItemCreater itemCreater,
            IAbilityCreater abilityCreater
        )
        {
            while (true)
            {
                IItem i = itemCreater.Create(
                    Console.ReadLine(),
                    abilityCreater
                );
                if (i != null && i.GetBattle() != null)
                {
                    return i;
                }
                else
                {
                    Console.WriteLine("无效装备，请重新选择");
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
            ICharacter char1 = this.CreateCharacter(
                characterCreater, itemCreater, abilityCreater);
            Console.WriteLine("请选择角色1的武器：");
            IItem item1 = this.CreateItem(
                itemCreater, abilityCreater);
            char1.GetEquipments().EquipItem("Main", item1);

            Console.WriteLine("请选择角色2：");
            ICharacter char2 = this.CreateCharacter(
                characterCreater, itemCreater, abilityCreater);
            Console.WriteLine("请选择角色2的武器：");
            IItem item2 = this.CreateItem(
                itemCreater, abilityCreater);
            char2.GetEquipments().EquipItem("Main", item2);

            ICharacterBattle battle1 = char1.GetBattle();
            ICharacterBattle battle2 = char2.GetBattle();

            if (battle1 == null || battle2 == null)
            {
                return null;
            }

            if (battle1.Dexterity.Value < battle2.Dexterity.Value)
            {
                // 敏捷高的排前面
                ICharacter charTmp = char1;
                char1 = char2;
                char2 = charTmp;
            }

            return new TestBattleStage(
                actionCreater, new ICharacter[2] { char1, char2 });
        }
    }
}