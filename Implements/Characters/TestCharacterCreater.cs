using System.Linq;
using System.Collections.Generic;

namespace Hathor
{
    class TestCharacterCreater : ICharacterCreater
    {
        protected Dictionary<string, ICharacterClass> mClasses;

        public ICharacter Create(string clsID)
        {
            var cls = this.FindClass(clsID);
            if (cls == null)
            {
                return null;
            }

            return null;
        }

        public ICharacterClass[] ListClass()
        {
            return this.mClasses.Values.ToArray();
        }

        public ICharacterClass FindClass(string clsID)
        {
            ICharacterClass cls = null;
            this.mClasses.TryGetValue(clsID, out cls);
            return cls;
        }
    }
}
