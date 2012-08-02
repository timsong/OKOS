namespace WFS
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string Format(this string format, params object[] args)
        {
            if (format.IsNullOrEmpty())
            {
                return string.Empty;
            }

            return string.Format(format, args);
        }
    }
}
