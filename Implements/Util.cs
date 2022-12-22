using System;

namespace Hathor
{
    public class Util
    {
        protected static Random rnd = new Random();

        public static string RamdonID()
        {
            var b64 = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var len = b64.Length / 2;
            return b64.Remove(b64.Length - len, len);
        }

        public static int RandomInt(int min, int max)
        {
            return Util.rnd.Next(min, max);
        }
    }
}
