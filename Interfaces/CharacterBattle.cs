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
        ICharacterAttribute MaxHP { get; }

        // 最大怒气
        ICharacterAttribute MaxAP { get; }

        // 力量
        ICharacterAttribute Strength { get; }

        // 智力
        ICharacterAttribute Intelligence { get; }

        // 敏捷
        ICharacterAttribute Dexterity { get; }

        // 获取额外属性值
        ICharacterAttribute GetAttribute(string attrName);

        // 更新属性
        void Update();
    }
}
