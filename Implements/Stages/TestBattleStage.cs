using System;
using System.Collections.Generic;

namespace Hathor
{
    class TestBattleActionEvent : IEvent
    {
        public string Series { get => "test"; }

        public ICharacter Attacker;

        public ICharacter Defender;
    }

    class TestBattleStage : IStage, IEventListener
    {
        protected IActionCreater mActionCreater;

        protected ICharacter[] mCharacters;

        public TestBattleStage(
            IActionCreater actionCreater,
            ICharacter[] characters
        )
        {
            this.mActionCreater = actionCreater;
            this.mCharacters = characters;
            foreach (var ch in characters)
            {
                ch.Subscribe(this);
            }
        }

        // 查找建筑
        public IBuilding FindBuilding(string buildingID)
        {
            return null;
        }

        // 查找角色
        public ICharacter FindCharacter(string characterID)
        {
            foreach (var ch in this.mCharacters)
            {
                if (ch.ID == characterID)
                {
                    return ch;
                }
            }

            return null;
        }

        // 查找物品
        public IItem FindItem(string itemID)
        {
            return null;
        }

        public void OnNotify(IEvent ev)
        {
            var actionEvent = ev as TestBattleActionEvent;
            if (actionEvent != null)
            {
                var actions = this.mActionCreater.CreateByCharacter(actionEvent.Attacker);
                // 选择行动
                for (int i = 0; i < actions.Length; i++)
                {
                    var action = actions[i];
                    Console.WriteLine("选择技能");
                    Console.WriteLine(string.Format("{0} {1}", i, action.Name));
                }

                IAction actionSelected = null;
                while (true)
                {
                    int actionIndex = Convert.ToInt32(Console.ReadLine());
                    if (actionIndex < 0 || actionIndex >= actions.Length)
                    {
                        Console.WriteLine("无效选项，请重新选择");
                        continue;
                    }
                    actionSelected = actions[actionIndex];
                    if (
                        !actionSelected.IsAppliableOnCharacter ||
                        !actionSelected.IsAppliable
                    )
                    {
                        Console.WriteLine(string.Format("{0} 无法释放", actionSelected.Name));
                        continue;
                    }
                    break;
                }

                if (actionSelected.IsAppliable)
                {
                    // 释放到自己身上
                    actionSelected.Apply();
                }
                else if (actionSelected.IsAppliableOnCharacter)
                {
                    // 释放到别人身上
                    actionSelected.ApplyOnCharacter(actionEvent.Defender);
                }

            }
            else
            {
                // 其他事件直接打印
                Console.WriteLine(ev.ToString());
            }
        }

        // 场景更新
        public void Update()
        {
            int attackerIndex = 0;
            int defenderIndex = 1;

            while (true)
            {
                // 持续更新2秒
                var startTime = DateTime.UtcNow.Ticks;
                while (true)
                {
                    foreach (var ch in this.mCharacters)
                    {
                        ch.Update();
                    }

                    var deltaTime = DateTime.UtcNow.Ticks - startTime;
                    if (deltaTime / 10000000.0 >= 2.0)
                    {// 更新满2秒钟
                        break;
                    }
                }

                // 等待指令
                var attacker = this.mCharacters[attackerIndex];
                var defender = this.mCharacters[defenderIndex];

                // 选择攻击技能释放
                this.OnNotify(new TestBattleActionEvent()
                {
                    Attacker = attacker,
                    Defender = defender
                });

                var tempIndex = attackerIndex;
                attackerIndex = defenderIndex;
                defenderIndex = tempIndex;
            }
        }
    }
}
