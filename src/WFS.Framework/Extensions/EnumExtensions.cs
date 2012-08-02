using System;
using System.Linq;

namespace WFS
{
    public static class EnumExtensions
    {
        public static TEnum ParseEnum<TEnum>(this string value)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new InvalidOperationException(string.Format("Type {0} is not a valid enum.", typeof(TEnum).Name));
            }

            if (value.IsNullOrEmpty())
            {
                return default(TEnum);
            }

            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        public static TEnum TryParseEnum<TEnum>(this string value)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new InvalidOperationException(string.Format("Type {0} is not a valid enum.", typeof(TEnum).Name));
            }

            if (!value.IsNullOrEmpty())
            {
                var exists = Enum.GetNames(typeof(TEnum))
                    .Any(item => item.ToLower() == value.ToLower());

                if (exists)
                {
                    return (TEnum)Enum.Parse(typeof(TEnum), value, true);
                }
            }

            return default(TEnum);
        }

        public static bool TryParseEnum<TEnum>(this string value, out TEnum result)
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new InvalidOperationException(string.Format("Type {0} is not a valid enum.", typeof(TEnum).Name));
            }

            if (!value.IsNullOrEmpty())
            {
                var exists = Enum.GetNames(typeof(TEnum))
                    .Any(item => item.ToLower() == value.ToLower());

                if (exists)
                {
                    result = (TEnum)Enum.Parse(typeof(TEnum), value, true);
                    return true;
                }
            }

            result = default(TEnum);
            return false;
        }
    }
}
