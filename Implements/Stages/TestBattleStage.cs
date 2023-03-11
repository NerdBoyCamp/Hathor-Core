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
        protected ICharacter mAttacker;
        protected ICharacter mDefender;

        protected enum EnumStatus {
            STATUS_ACT,
            STATUS_WAIT,
        };
        protected EnumStatus mStatus;
        protected float mStatusTime;  // current status running time

        public TestBattleStage(
            IActionCreater actionCreater,
            ICharacter[] characters
        )
        {
            this.mActionCreater = actionCreater;
            this.mAttacker = characters[0];
            this.mDefender = characters[1];
            this.mStatus = EnumStatus.STATUS_ACT;
            this.mStatusTime = 0.0f;

            this.mAttacker.Subscribe(this);
            this.mDefender.Subscribe(this);
        }

        // 查找建筑
        public IBuilding FindBuilding(string buildingID)
        {
            return null;
        }

        // 查找角色
        public ICharacter FindCharacter(string characterID)
        {
            if (this.mAttacker.ID == characterID)
            {
                return this.mAttacker;
            }

            if (this.mDefender.ID == characterID)
            {
                return this.mDefender;
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
            if (ev is TestBattleActionEvent actionEvent)
            {
                Console.WriteLine(string.Format(
                    "{0}选择技能攻击{1}：",
                    actionEvent.Attacker.Name,
                    actionEvent.Defender.Name));
                
                IAction[] actions = this.mActionCreater.CreateByCharacter(
                    actionEvent.Attacker);
                // 选择行动
                for (int i = 0; i < actions.Length; i++)
                {
                    IAction action = actions[i];
                    Console.WriteLine(string.Format("{0} {1}", i, action.Name));
                }

                IAction actionSelected;
                while (true)
                {
                    try
                    {
                        int actionIndex = Convert.ToInt32(Console.ReadLine());
                        if (actionIndex < 0 || actionIndex >= actions.Length)
                        {
                            Console.WriteLine("无效选项，请重新选择");
                            continue;
                        }
                        actionSelected = actions[actionIndex];
                        if (
                            !actionSelected.IsAppliableOnCharacter &&
                            !actionSelected.IsAppliable
                        )
                        {
                            Console.WriteLine(string.Format("{0} 无法释放", actionSelected.Name));
                            continue;
                        }
                        break;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("无效选项，请重新选择");
                    }
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
        public void Update(float deltaTime)
        {
            switch (mStatus)
            {
                case EnumStatus.STATUS_WAIT:
                    this.mAttacker.Update(deltaTime);
                    this.mDefender.Update(deltaTime);
                    this.mStatusTime += deltaTime;
                    if (this.mStatusTime > 2.0f)
                    {
                        // 持续更新2秒跳转到等待指令状态
                        this.mStatus = EnumStatus.STATUS_ACT;
                        this.mStatusTime = 0.0f;
                        // 显示当前状态
                        Console.WriteLine(string.Format(
                            "{0}剩余HP：{1}",
                            this.mAttacker.Name,
                            this.mAttacker.GetBattle().HP.Value));
                        Console.WriteLine(string.Format(
                            "{0}剩余HP：{1}",
                            this.mDefender.Name,
                            this.mDefender.GetBattle().HP.Value));
                    }
                    break;
                case EnumStatus.STATUS_ACT:
                    // 处理玩家指令
                    this.OnNotify(new TestBattleActionEvent()
                    {
                        Attacker = this.mAttacker,
                        Defender = this.mDefender,
                    });
                    this.mStatus = EnumStatus.STATUS_WAIT;
                    this.mStatusTime = 0.0f;
                    // 转换角色
                    var character = this.mAttacker;
                    this.mAttacker = this.mDefender;
                    this.mDefender = character;
                    break;
            }
        }
    }
}
