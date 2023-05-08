using System.Reflection;
using GameDot.Core.Attributes;

namespace GameDot.Api.Helpers
{
    public static class EnumExtender
    {
        /// <summary>
        /// Returns the value of EnumStringValueAttribute or the enumValue.ToString()
        /// </summary>
        public static string GetStringValue<T>(this T enumerationValue) where T : struct
        {
            Type type = enumerationValue.GetType();

            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            // Tries to find a EnumStringValueAttribute for a potential friendly name for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());

            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(EnumStringValueAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    string str = ((EnumStringValueAttribute)attrs[0]).Value;

                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        return str;
                    }
                }
            }

            // If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }
    }
}
