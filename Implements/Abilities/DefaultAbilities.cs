using System;
using System.Linq;
using System.Collections.Generic;

namespace Hathor
{
    class DefaultAbilities : IAbilities
    {
        protected Dictionary<string, IEffectClass> mAbilities;

        public DefaultAbilities()
        {
            this.mAbilities = new Dictionary<string, IEffectClass>();
        }

        // 添加能力
        public IEffectClass AddAbility(IEffectClass effectClass)
        {
            try
            {
                this.mAbilities[effectClass.ID] = effectClass;
            }
            catch (Exception)
            {
                return null;
            }
            return effectClass;
        }

        // 删除能力
        public IEffectClass RemoveAbility(string effectClassID)
        {
            IEffectClass ability = this.GetAbility(effectClassID);
            if (ability == null)
            {
                return null;
            }
            this.mAbilities.Remove(effectClassID);
            return ability;
        }

        // 查找能力
        public IEffectClass GetAbility(string effectClassID)
        {
            IEffectClass ability = null;
            if (!this.mAbilities.TryGetValue(effectClassID, out ability))
            {
                return null;
            }
            return ability;
        }

        // 获取所有能力
        public IEffectClass[] ListAbilities()
        {
            return this.mAbilities.Values.ToArray();
        }
    }
}
