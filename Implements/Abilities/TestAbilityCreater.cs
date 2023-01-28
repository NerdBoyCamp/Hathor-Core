using System;
using System.Collections.Generic;

namespace Hathor
{
    class TestAbilityCreater : IAbilityCreater
    {
        protected Dictionary<string, dynamic> mConfigs;

        protected Dictionary<string, IEffectClass> mCache;

        protected IEffectClass DoCreate(string templateId)
        {
            try
            {
                this.mConfigs.TryGetValue(templateId, out dynamic configs);
                if (configs == null)
                {
                    return null;
                }

                string configsType = Util.GetConfigAsString(configs, "Type");
                switch (configsType)
                {
                    case "Promote":
                        return new PromoteEffectClass(
                            Util.RandomID(),
                            Util.GetConfigAsString(configs, "Name", templateId),
                            new Dictionary<string, float>
                            {
                                { "Prcp", Util.GetConfigAsFloat(configs, "PerceptionIncrease") },
                                { "Luck", Util.GetConfigAsFloat(configs, "LuckIncrease") },
                                { "Elqn", Util.GetConfigAsFloat(configs, "EloquenceIncrease") },
                                { "Appr", Util.GetConfigAsFloat(configs, "AppearanceIncrease") },
                            },
                            new Dictionary<string, float>
                            {
                                { "Prcp", Util.GetConfigAsFloat(configs, "PerceptionExpand") },
                                { "Luck", Util.GetConfigAsFloat(configs, "LuckExpand") },
                                { "Elqn", Util.GetConfigAsFloat(configs, "EloquenceExpand") },
                                { "Appr", Util.GetConfigAsFloat(configs, "AppearanceExpand") },
                            },
                            new Dictionary<string, float>
                            {
                                { "MaxHP", Util.GetConfigAsFloat(configs, "MaxHPIncrease") },
                                { "MaxAP", Util.GetConfigAsFloat(configs, "MaxAPIncrease") },
                                { "Str", Util.GetConfigAsFloat(configs, "StrengthIncrease") },
                                { "Int", Util.GetConfigAsFloat(configs, "IntelligenceIncrease") },
                                { "Dex", Util.GetConfigAsFloat(configs, "DexterityIncrease") },
                                { "CrtRt", Util.GetConfigAsFloat(configs, "CriticalHitRateIncrease") },
                                { "CrtD", Util.GetConfigAsFloat(configs, "CriticalHitDamageIncrease") },
                                { "Spd", Util.GetConfigAsFloat(configs, "SpeedIncrease") },
                                { "PhyD", Util.GetConfigAsFloat(configs, "PhysicalDamageIncrease") },
                                { "PhyR", Util.GetConfigAsFloat(configs, "PhysicalResistenceIncrease") },
                                { "MagD", Util.GetConfigAsFloat(configs, "MagicalDamageIncrease") },
                                { "MagR", Util.GetConfigAsFloat(configs, "MagicalResistenceIncrease") },
                                { "IceD", Util.GetConfigAsFloat(configs, "IceDamageIncrease") },
                                { "PsnD", Util.GetConfigAsFloat(configs, "PoisonDamageIncrease") },
                                { "FirD", Util.GetConfigAsFloat(configs, "FireDamageIncrease") },
                                { "RdiD", Util.GetConfigAsFloat(configs, "RadiaionDamageIncrease") },
                            },
                            new Dictionary<string, float>
                            {
                                { "MaxHP", Util.GetConfigAsFloat(configs, "MaxHPExpand") },
                                { "MaxAP", Util.GetConfigAsFloat(configs, "MaxAPExpand") },
                                { "Str", Util.GetConfigAsFloat(configs, "StrengthExpand") },
                                { "Int", Util.GetConfigAsFloat(configs, "IntelligenceExpand") },
                                { "Dex", Util.GetConfigAsFloat(configs, "DexterityExpand") },
                                { "CrtRt", Util.GetConfigAsFloat(configs, "CriticalHitRateExpand") },
                                { "CrtD", Util.GetConfigAsFloat(configs, "CriticalHitDamageExpand") },
                                { "Spd", Util.GetConfigAsFloat(configs, "SpeedExpand") },
                                { "PhyD", Util.GetConfigAsFloat(configs, "PhysicalDamageExpand") },
                                { "PhyR", Util.GetConfigAsFloat(configs, "PhysicalResistenceExpand") },
                                { "MagD", Util.GetConfigAsFloat(configs, "MagicalDamageExpand") },
                                { "MagR", Util.GetConfigAsFloat(configs, "MagicalResistenceExpand") },
                                { "IceD", Util.GetConfigAsFloat(configs, "IceDamageExpand") },
                                { "PsnD", Util.GetConfigAsFloat(configs, "PoisonDamageExpand") },
                                { "FirD", Util.GetConfigAsFloat(configs, "FireDamageExpand") },
                                { "RdiD", Util.GetConfigAsFloat(configs, "RadiaionDamageExpand") },
                            }
                        );
                    case "Damage":
                        return new DamageEffectClass(
                            Util.RandomID(),
                            Util.GetConfigAsString(configs, "Name", templateId),
                            Util.GetConfigAsString(configs, "Series", "Phy"),
                            Util.GetConfigAsFloat(configs, "MaxDamage"),
                            Util.GetConfigAsFloat(configs, "MinDamage")
                        );
                    default:
                        throw new Exception(string.Format(
                            "unknown ability type {0}", configsType));
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.ToString());
                return null;
            }
        }

        public TestAbilityCreater()
        {
            this.mCache = new Dictionary<string, IEffectClass>();
            this.mConfigs = new Dictionary<string, dynamic>
            {
                {
                    "TestAbility1", new {
                        Name = "TestAbility1",
                        Type = "Promote",
                        MaxHPIncrease = 10,
                        MaxAPIncrease = 10,
                        PerceptionIncrease = 0,
                        LuckIncrease = 0,
                        EloquenceIncrease = 0,
                        AppearanceIncrease = 0,
                        StrengthIncrease = 5,
                        IntelligenceIncrease = 5,
                        DexterityIncrease = 0,
                        CriticalHitRateIncrease = 0,
                        CriticalHitDamageIncrease = 0,
                        SpeedIncrease = 0,
                        PhysicalDamageIncrease = 0,
                        PhysicalResistenceIncrease = 0,
                        MagicalDamageIncrease = 0,
                        MagicalResistenceIncrease = 0,
                        IceDamageIncrease = 0,
                        PoisonDamageIncrease = 0,
                        FireDamageIncrease = 0,
                        RadiaionDamageIncrease = 0,
                    }
                },
                {
                    "TestAbility2", new {
                        Name = "TestAbility2",
                        Type = "Promote",
                        MaxHPIncrease = 0,
                        MaxAPIncrease = 0,
                        PerceptionIncrease = 0,
                        LuckIncrease = 0,
                        EloquenceIncrease = 0,
                        AppearanceIncrease = 0,
                        StrengthIncrease = 0,
                        IntelligenceIncrease = 8,
                        DexterityIncrease = 9,
                        CriticalHitRateIncrease = 0,
                        CriticalHitDamageIncrease = 0,
                        PhysicalDamageIncrease = 10,
                        PhysicalResistenceIncrease = 0,
                        PhysicalResistenceExpand = 0.1,
                        MagicalDamageIncrease = 10,
                        MagicalResistenceIncrease = 0,
                        MagicalResistenceExpand = 0.05,
                        SpeedIncrease = 0,
                        IceDamageIncrease = 0,
                        PoisonDamageIncrease = 0,
                        FireDamageIncrease = 0,
                        RadiaionDamageIncrease = 0,
                    }
                },
                {
                    "TestAbility3", new {
                        Name = "TestAbility3",
                        Type = "Promote",
                        MaxHPIncrease = 50,
                        MaxAPIncrease = 0,
                        PerceptionIncrease = 0,
                        LuckIncrease = 0,
                        EloquenceIncrease = 0,
                        AppearanceIncrease = 0,
                        StrengthIncrease = 15,
                        IntelligenceIncrease = -15,
                        DexterityIncrease = 15,
                        CriticalHitRateIncrease = 0.03,
                        CriticalHitDamageIncrease = 0,
                        PhysicalDamageIncrease = 0,
                        PhysicalResistenceIncrease = 0,
                        MagicalDamageIncrease = 0,
                        MagicalResistenceIncrease = 0,
                        SpeedIncrease = -15,
                        IceDamageIncrease = 0,
                        PoisonDamageIncrease = 0,
                        FireDamageIncrease = 0,
                        RadiaionDamageIncrease = 0,
                    }
                },
                {
                    "TestAbility4", new {
                        Name = "TestAbility4",
                        Type = "Promote",
                        MaxHPIncrease = 70,
                        MaxAPIncrease = 0,
                        PerceptionIncrease = 0,
                        LuckIncrease = 0,
                        EloquenceIncrease = 0,
                        AppearanceIncrease = 0,
                        StrengthIncrease = -10,
                        IntelligenceIncrease = 5,
                        DexterityIncrease = 20,
                        CriticalHitRateIncrease = -0.05,
                        CriticalHitDamageIncrease = 0,
                        PhysicalDamageIncrease = 5,
                        PhysicalResistenceExpand = -0.05,
                        MagicalDamageIncrease = 5,
                        MagicalResistenceExpand = 0.1,
                        SpeedIncrease = 20,
                        IceDamageIncrease = 0,
                        PoisonDamageIncrease = 0,
                        FireDamageIncrease = 0,
                        RadiaionDamageIncrease = 0,
                    }
                },
                {
                    "TestAbility5", new {
                        Name = "TestAbility5",
                        Type = "Promote",
                        MaxHPIncrease = -50,
                        MaxAPIncrease = 0,
                        PerceptionIncrease = 0,
                        LuckIncrease = 0,
                        EloquenceIncrease = 0,
                        AppearanceIncrease = 0,
                        StrengthIncrease = 20,
                        IntelligenceIncrease = 30,
                        DexterityIncrease = 25,
                        CriticalHitRateIncrease = 0.03,
                        CriticalHitDamageIncrease = 0,
                        PhysicalDamageIncrease = 0,
                        PhysicalResistenceIncrease = 0,
                        MagicalDamageIncrease = 0,
                        MagicalResistenceIncrease = 0,
                        SpeedIncrease = -15,
                        IceDamageIncrease = 0,
                        PoisonDamageIncrease = 0,
                        FireDamageIncrease = 0,
                        RadiaionDamageIncrease = 0,
                    }
                },
                {
                    "SkillsTest1", new {
                        Name = "SkillsTest1",
                        Type = "Damage",
                        Series = "Phy",
                        MinDamage = 15,
                        MaxDamage = 15,
                    }
                },
                {
                    "SkillsTest2", new {
                        Name = "SkillsTest2",
                        Type = "Damage",
                        Series = "Mag",
                        MinDamage = 40,
                        MaxDamage = 40,
                    }
                },
            };
        }

        public IEffectClass Create(string templateId)
        {
            this.mCache.TryGetValue(templateId, out IEffectClass ability);
            if (ability != null)
            {
                return ability;
            }

            ability = this.DoCreate(templateId);
            this.mCache[templateId] = ability;
            return ability;
        }
    }
}
