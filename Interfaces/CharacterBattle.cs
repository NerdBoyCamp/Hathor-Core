using System;

namespace Hathor
{
    // 战斗相关
    public interface ICharacterBattle
    {
        // ---------------------------------------------------
        // 血量
        ICharacterHealth HP { get; }
        
        // 怒气
        ICharacterAnger AP { get; }

        // 最大血量
        IAttribute MaxHP { get; }

        // 最大怒气
        IAttribute MaxAP { get; }

        // 力量
        IAttribute Strength { get; }

        // 智力
        IAttribute Intelligence { get; }

        // 敏捷
        IAttribute Dexterity { get; }

        // 获取额外属性值
        IAttribute GetAttribute(string attrName);

        // 更新属性
        void Update();
    }
}
