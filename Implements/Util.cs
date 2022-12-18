using System;

namespace Hathor
{
    public class Util
    {
        public static string RamdonID()
        {
            var b64 = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var len = b64.Length / 2;
            return b64.Remove(b64.Length - len, len);
        }
    }
}
