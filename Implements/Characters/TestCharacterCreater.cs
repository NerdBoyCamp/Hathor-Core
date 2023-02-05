using System;
using System.Collections;
using System.Collections.Generic;

namespace Hathor
{
    class TestCharacterCreater : ICharacterCreater
    {
        protected Dictionary<string, dynamic> mConfigs;

        protected ICharacterClass mCls = new DefaultCharacterClass();

        public TestCharacterCreater()
        {
            this.mConfigs = new Dictionary<string, object>
            {
                {
                    "Some1", new
                    {
                        Name = "Some1",
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
                        CriticalHitDamage = 2.0f,
                        CriticalHitRate = 0.05f,
                        Abilities = new string[]
                        {
                            "TestAbility1",
                            "TestAbility2",
                            "TestAbility3",
                        }
                    }
                },
                {
                    "Some2", new
                    {
                        Name = "Some2",
                        HP = 100,
                        MaxHP = 100,
                        AP = 100,
                        MaxAP = 100,
                        EXP = 0,
                        LV = 1,
                        AvailablePoints = 0,
                        Perception = 65,
                        Luck = 32,
                        Eloquence = 56,
                        Appearance = 77,
                        Strength = 5,
                        Dexterity = 6,
                        Intelligence = 13,
                        Speed = 60,
                        CriticalHitDamage = 2.0f,
                        CriticalHitRate = 0.05f,
                        Abilities = new string[]
                        {
                            "TestAbility4",
                            "TestAbility5",
                            // "TestAbility6",
                        }
                    }
                },
            };

            Console.WriteLine("??????????");

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
            try
            {
                this.mConfigs.TryGetValue(templateId, out dynamic configs);
                if (configs == null)
                {
                    return null;
                }

                ICharacter c = this.mCls.Create(configs);
                if (c == null)
                {
                    return null;
                }

                IAbilities abilities = c.GetAbilities();
                if (abilities != null)
                {
                    if (Util.GetConfigAsObject(configs, "Abilities") is IEnumerable iter)
                    {
                        foreach (var i in iter)
                        {
                            if (!(i is string abilityName))
                            {
                                throw new Exception("invalid character ability found");
                            }

                            IEffectClass ability =
                                abilityCreater.Create(abilityName);
                            if (ability == null)
                            {
                                throw new Exception(string.Format(
                                    "invalid character ability {0}", abilityName));
                            }

                            bool isApplied = false;
                            if (
                                ability.IsAuto &&
                                ability.IsSelf &&
                                ability.IsAppliableOnCharacter
                            )
                            {
                                // ????????????
                                IEffects effects = c.GetEffects();
                                IEffect effect = ability.CreateByCharacter(c);
                                if (effect != null && effects != null)
                                {
                                    effects.AddEffect(effect);
                                    isApplied = true;
                                }
                            }

                            if (!isApplied)
                            {
                                // ??????????????????????????
                                abilities.AddAbility(ability);
                            }
                        }
                    }
                }

                return c;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
