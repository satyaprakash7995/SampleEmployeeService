using SampleEmployeeService.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Domain.Helpers
{
    public static class EnumHelper<TEnum> where TEnum : struct, IConvertible
    {
        public static TEnum? GetValue(string description)
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("TEnum must be an enum");

            var enumValues = Enum.GetValues(typeof(TEnum));
            foreach (var enumValue in enumValues)
                if (((Enum)enumValue).GetDescription() == description)
                    return (TEnum)enumValue;

            return null;
        }

        public static IEnumerable<TEnum> GetEnumValues()
        {
            var result = Enum.GetValues(typeof(TEnum)).OfType<TEnum>();
            return result;
        }

        public static TEnum Parse(string value)
        {
            TEnum result;
            try
            {
                result = (TEnum)Enum.Parse(typeof(TEnum), value, true);
            }
            catch (Exception)
            {
                try
                {
                    result = ParseDisplayValues(value, true);
                }
                catch (Exception)
                {
                    return default;
                }
            }

            return result;
        }

        public static IDictionary<string, TEnum> GetValues(bool ignoreCase)
        {
            var enumValues = new Dictionary<string, TEnum>();

            foreach (var fi in typeof(TEnum).GetFields(BindingFlags.Static | BindingFlags.Public))
            {
                var key = fi.Name;

                if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] display)
                    key = display.Length > 0 ? display[0].Description : fi.Name;

                if (ignoreCase)
                    key = key.ToLower();

                if (!enumValues.ContainsKey(key))
                    enumValues[key] = (TEnum)fi.GetRawConstantValue()!;
            }

            return enumValues;
        }

        private static TEnum ParseDisplayValues(string value, bool ignoreCase)
        {
            var values = GetValues(ignoreCase);

            var key = ignoreCase ? value.ToLower() : value;

            if (values.ContainsKey(key))
                return values[key];

            throw new ArgumentException(value);
        }

    }
}
