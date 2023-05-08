namespace GameDot.Core.Helpers
{
    public class EnumUtility
    {
        public static List<TEnum> EnumToList<TEnum>() where TEnum : struct
        {
            List<TEnum> result = new List<TEnum>();

            Array arr = Enum.GetValues(typeof(TEnum));

            foreach (int i in arr)
            {
                result.Add((TEnum)Enum.Parse(typeof(TEnum), i.ToString()));
            }

            return result;
        }

        public static Nullable<TEnum> ParseOrNull<TEnum>(string value) where TEnum : struct
        {
            Nullable<TEnum> result = null;

            TEnum tmp;

            if (!string.IsNullOrEmpty(value) && Enum.TryParse<TEnum>(value, out tmp))
            {
                result = tmp;
            }

            return result;
        }

        public static Nullable<TEnum> ParseOrNull<TEnum>(int? value) where TEnum : struct
        {
            Nullable<TEnum> result = null;

            if (value.HasValue)
            {
                result = ParseOrNull<TEnum>(Convert.ToString(value.Value));
            }

            return result;
        }

        public static bool IsEnumInRange<TEnum>(TEnum type, List<TEnum> allowedTypes) where TEnum : struct
        {
            return allowedTypes.Contains(type);
        }
    }
}
