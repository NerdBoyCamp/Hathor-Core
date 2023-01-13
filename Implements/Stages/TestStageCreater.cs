namespace Hathor
{
    class TestStageCreater : IStageCreater
    {
        // 创建场景
        public IStage Create(
            IBuildingCreater buildingCreater,
            ICharacterCreater characterCreater,
            IItemCreater itemCreater,
            IActionCreater actionCreater
        )
        {
            var char1 = characterCreater.Create("test");
            var char2 = characterCreater.Create("test");

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