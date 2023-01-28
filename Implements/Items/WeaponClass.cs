using System;
using System.Collections.Generic;

namespace Hathor
{
    // 用户配置字段，仅供参考。不会被使用
    class WeaponConfig
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float MinPhysicalDamage { get; set; }
        public float MaxPhysicalDamage { get; set; }
        public float MinMagicalDamage { get; set; }
        public float MaxMagicalDamage { get; set; }
        public float StrengthRequired { get; set; }
        public float StrengthIncrease { get; set; }
        public float IntelligenceRequired { get; set; }
        public float IntelligenceIncrease { get; set; }
        public float DexterityRequired { get; set; }
        public float DexterityIncrease { get; set; }
        public float PerceptionIncrease { get; set; }
        public float LuckIncrease { get; set; }
    }

    class WeaponClass : IItemClass
    {
        public string ID { get => "Weapon"; }

        // 名字
        public string Name { get => "Weapon Item Class"; }

        // 描述
        public string Desctiption { get => this.Name; }

        // 类别 头盔/武器/胸甲/鞋子/腰带/回复道具/...
        public string Series { get => "Weapon"; }

        // 创建实例
        public IItem Create(dynamic configs)
        {
            try
            {
                DefaultItem item = new(
                    this, Util.RandomID(),
                    (string)configs.Name,
                    (string)configs.Description
                );
                item.mAbilities = new DefaultAbilities();
                item.mAbilities.AddAbility(new DamageEffectClass(
                    Util.RandomID(),
                    "Attack",
                    "Phy",
                    (float)configs.MaxPhysicalDamage,
                    (float)configs.MinPhysicalDamage
                ));
                item.mBattle = new DefaultItemBattle(
                    item,
                    new Dictionary<string, float>
                    {
                        { "Str", (float)configs.StrengthRequired },
                        { "Int", (float)configs.IntelligenceRequired },
                        { "Dex", (float)configs.DexterityRequired },
                    },
                    new Dictionary<string, float>
                    {
                        { "Str", (float)configs.StrengthIncrease },
                        { "Int", (float)configs.IntelligenceIncrease },
                        { "Dex", (float)configs.DexterityIncrease },
                    }
                );
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
