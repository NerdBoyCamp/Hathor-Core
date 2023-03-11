using System.Collections.Generic;

namespace Hathor
{
    class DefaultCharacterEffects : DefaultEffects
    {
        // 角色
        protected ICharacter mChar;

        // 构造
        public DefaultCharacterEffects(ICharacter character)
        {
            this.mChar = character;
        }

        // 添加效果
        public override IEffect AddEffect(IEffect effect)
        {
            if (!effect.GetClass().IsAppliableOnCharacter)
            {
                // 无法对角色产生影响
                return null;
            }

            return base.AddEffect(effect);
        }

        // 删除效果
        protected override void ApplyEffect(IEffect effect, float deltaTime)
        {
            effect.UpdateOnCharacter(this.mChar, deltaTime);

            // 发送消息通知
            this.mChar.Publish(new EffectEvent { Effect = effect });
        }
    }
}
