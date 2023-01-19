using System;
using System.Collections.Generic;

namespace Hathor
{
    class TestCharacterCreater : ICharacterCreater
    {
        protected Dictionary<string, object> mConfigs;

        protected ICharacterClass mCls = new DefaultCharacterClass();

        public TestCharacterCreater()
        {
            this.mConfigs = new Dictionary<string, object>
            {
                {
                    "some1", new DefaultCharacterConfig
                    {
                        Name = "some1",
                        HP = 100,
                        MaxHP = 100,
                        AP = 100,
                        MaxAP = 100,
                        EXP = 0,
                        LV = 1,
                        AvailablePoints = 0,
                        Perception = 83,
                        Luck = 64,
                        Eloquence = 26,
                        Appearance = 75,
                        Strength = 8,
                        Dexterity = 12,
                        Intelligence = 5,
                        Speed = 50,
                        CriticalHitDamage = 0,
                        CriticalHitRate = 0.05f,
                    }
                }
            };

            Console.WriteLine("可选角色：");

            foreach(var c in this.mConfigs)
            {
                Console.WriteLine(c.Key);
            }
        }

        public ICharacter Create(
            string templateId,
            IItemCreater itemCreater,
            IAbilityCreater abilityCreater
        )
        {
            this.mConfigs.TryGetValue(templateId, out object configs);
            if (configs == null)
            {
                return null;
            }

            var c = this.mCls.Create(configs);
            if (c == null)
            {
                return null;
            }

            // TODO: 添加能力

            return c;
        }
    }
}
