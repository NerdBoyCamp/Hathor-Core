using System;

namespace Hathor
{
    public class Util
    {
        protected static readonly Random rnd = new Random();

        public static string RandomID()
        {
            var b64 = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            var len = b64.Length / 2;
            return b64.Remove(b64.Length - len, len);
        }

        public static int RandomInt(int min, int max)
        {
            return Util.rnd.Next(min, max);
        }

        public static double RandomDouble(double min, double max)
        {
            double delta = max - min;
            return Util.rnd.NextDouble() * delta + min;
        }

        public static object GetConfigAsObject(
            object configs,
            string attrName,
            object defaultVal = null
        )
        {
            var attr = configs.GetType().GetProperty(attrName);
            if (attr == null)
            {
                return null;
            }

            var attrVal = attr.GetValue(configs);
            if (attrVal == null)
            {
                return defaultVal;
            }

            return attrVal;
        }

        public static string GetConfigAsString(
            object configs,
            string attrName,
            string defaultVal = ""
        )
        {
            var attr = configs.GetType().GetProperty(attrName);
            if (attr == null)
            {
                return null;
            }

            var attrVal = attr.GetValue(configs);
            if (attrVal == null || attrVal.GetType() != typeof(string))
            {
                return defaultVal;
            }

            return (string)attrVal;
        }

        public static float GetConfigAsFloat(
            object configs,
            string attrName,
            float defaultVal = 0
        )
        {
            var attr = configs.GetType().GetProperty(attrName);
            if (attr == null)
            {
                return defaultVal;
            }
            var attrVal = attr.GetValue(configs, null);
            if (attrVal == null)
            {
                return defaultVal;
            }

            var attrValType = attrVal.GetType();
            if (
                attrValType != typeof(float) &&
                attrValType != typeof(double) &&
                attrValType != typeof(int) &&
                attrValType != typeof(uint)
            )
            {
                return defaultVal;
            }

            return Convert.ToSingle(attrVal);
        }

        public static double GetConfigAsDouble(
            object configs,
            string attrName,
            double defaultVal = 0
        )
        {
            var attr = configs.GetType().GetProperty(attrName);
            if (attr == null)
            {
                return defaultVal;
            }
            var attrVal = attr.GetValue(configs, null);
            if (attrVal == null)
            {
                return defaultVal;
            }

            var attrValType = attrVal.GetType();
            if (
                attrValType != typeof(float) &&
                attrValType != typeof(double) &&
                attrValType != typeof(int) &&
                attrValType != typeof(uint)
            )
            {
                return defaultVal;
            }

            return Convert.ToDouble(attrVal);
        }
    }
}
