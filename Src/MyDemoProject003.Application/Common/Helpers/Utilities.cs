using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace MyDemoProject003.Application.Common.Helpers
{
    public static class Utilities
    {
        public static bool IsEmpty(this string value) => string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value);

        public static bool IsNotEmpty(this string value) => !IsEmpty(value);

        public static string AsString(this object value) => value.AsString(string.Empty);

        public static string AsString(this object value, [NotNull] string defaultValue)
        {
            switch (value)
            {
                case null:
                    return defaultValue;

                case string s:
                    return s;

                default:
                    try
                    {
                        return Convert.ToString(value);
                    }
                    catch
                    {
                        return defaultValue;
                    }
            }
        }

        public static bool IsGuidValid(this string valueGuid)
        {
            return Guid.TryParse(valueGuid, out _);
        }

    }
}
