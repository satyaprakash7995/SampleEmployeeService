using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Domain.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName<T>(this T e) where T : IConvertible
        {
            var displayName = string.Empty;
            if (e is not Enum)
                return displayName;

            var type = e.GetType();
            var values = Enum.GetValues(type);
            foreach (int val in values)
            {
                if (val != e.ToInt32(CultureInfo.InvariantCulture))
                    continue;

                var memInfo = type.GetMember(type.GetEnumName(val));
                if (memInfo[0]
                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                        .FirstOrDefault() is DisplayAttribute displayAttribute)
                    displayName = displayAttribute.GetName();

                break;
            }

            return displayName;
        }

        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
        public static T GetAttribute<T>(this Enum enumVal) where T : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(Enum.GetName(type, enumVal));
            var result = memInfo.FirstOrDefault()?.GetCustomAttribute<T>(false);
            return result;
        }
        public static TOut GetAttributeValue<T, TOut>(this Enum enumVal,
        Func<T, TOut> selector, TOut defaultValue = default) where T : Attribute
        {
            var attr = GetAttribute<T>(enumVal);

            return attr == null || selector == null ? defaultValue : selector(attr);
        }

        public static string GetDescription(this Enum enumVal)
        {
            return GetAttributeValue<DescriptionAttribute, string>(enumVal, description => description.Description)
                   ?? enumVal.ToString();
        }
    }
}
