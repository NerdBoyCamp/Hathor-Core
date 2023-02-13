using System;
using System.Collections;
using System.Collections.Generic;

namespace Hathor
{
    class DefaultItemCreater : IItemCreater
    {
        protected Dictionary<string, object> mConfigs;

        protected Dictionary<string, IItemClass> mClasses;

        public DefaultItemCreater()
        {
            this.mConfigs = new Dictionary<string, object>
            {
                // TODO: ....
            };

            this.mClasses = new Dictionary<string, IItemClass>
            {
                {  "Weapon", new WeaponClass() },
            };
        }

        public IItem Create(
            string templateId,
            IAbilityCreater abilityCreater
        )
        {
            try
            {
                this.mConfigs.TryGetValue(templateId, out object configs);
                if (configs == null)
                {
                    return null;
                }

                this.mClasses.TryGetValue(
                    Util.GetConfigAsString(configs, "Type"),
                    out IItemClass cls
                );
                if (cls == null)
                {
                    return null;
                }

                IItem item = cls.Create(configs);
                if (item == null)
                {
                    return null;
                }

                IAbilities abilities = item.GetAbilities();
                if (abilities != null)
                {
                    if (Util.GetConfigAsObject(configs, "Abilities") is IEnumerable iter)
                    {
                        foreach (var abilityName in iter)
                        {
                            var ability = abilityCreater.Create(
                                abilityName as string);

                            if (ability != null)
                            {
                                abilities.AddAbility(ability);
                            }
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
