using System;
using System.Text;

namespace WFS
{
    public static class ExceptionExtensions
    {
        public static string FlattenExceptionMessage(this Exception exception)
        {
            var stringBuilder = new StringBuilder();
            var localException = exception;

            while (localException != null)
            {
                stringBuilder.AppendLine(localException.Message);
                localException = localException.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}
