namespace GameDot.Core.Attributes
{
    public class EnumStringValueAttribute : Attribute
    {
        public string Value { get; set; }

        public EnumStringValueAttribute(string value)
        {
            this.Value = value;
        }
    }
}
