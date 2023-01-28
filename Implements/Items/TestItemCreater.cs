using System;
using System.Collections;
using System.Collections.Generic;

namespace Hathor
{
    class TestItemCreater : IItemCreater
    {
        protected Dictionary<string, dynamic> mConfigs;

        protected IItemClass mCls;

        public TestItemCreater()
        {
            this.mCls = new WeaponClass();
            this.mConfigs = new Dictionary<string, dynamic>
            {
                {
                    "TestArms1", new {
                        Name="TestArms1",
                        Description="A weapon for testing",
                        MinPhysicalDamage=15,
                        MaxPhysicalDamage=25,
                        MinMagicalDamage=10,
                        MaxMagicalDamage=20,
                        StrengthRequired=10,
                        StrengthIncrease=5,
                        IntelligenceRequired=10,
                        IntelligenceIncrease=10,
                        DexterityRequired=5,
                        DexterityIncrease=5,
                        PerceptionIncrease=0,
                        LuckIncrease=0,
                        Abilities=new string[]
                        {
                            "SkillsTest1",
                        }
                    }
                },
                {
                    "TestArms2", new {
                        Name="TestArms2",
                        Description="B weapon for testing",
                        MinPhysicalDamage=0,
                        MaxPhysicalDamage=50,
                        MinMagicalDamage=0,
                        MaxMagicalDamage=5,
                        StrengthRequired=30,
                        StrengthIncrease=20,
                        IntelligenceRequired=5,
                        IntelligenceIncrease=0,
                        DexterityRequired=20,
                        DexterityIncrease=10,
                        PerceptionIncrease=0,
                        LuckIncrease=0,
                        Abilities=new string[]
                        {
                            "SkillsTest2",
                        }
                    } 
                },
            };

            Console.WriteLine("可选武器：");

            foreach (var c in this.mConfigs)
            {
                Console.WriteLine(c.Key);
            }
        }

        public IItem Create(
            string templateId,
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

                IItem item = this.mCls.Create(configs);
                if (item == null)
                {
                    return null;
                }

                IAbilities abilities = item.GetAbilities();
                if (abilities != null)
                {
                    if (Util.GetConfigAsObject(configs, "Abilities") is IEnumerable iter)
                    {
                        foreach (var i in iter)
                        {
                            if (i is not string abilityName)
                            {
                                throw new Exception("invalid item ability found");
                            }

                            IEffectClass ability = 
                                abilityCreater.Create(abilityName);
                            if (ability == null)
                            {
                                throw new Exception(string.Format(
                                    "invalid item ability {0}", abilityName));
                            }

                            abilities.AddAbility(ability);
                        }
                    }
                }
                return item;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
