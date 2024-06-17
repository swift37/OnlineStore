using OnlineStore.MVC.Models.Enums.Atributes;
using System.Reflection;

namespace OnlineStore.MVC.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? GetStringValue(this Enum value)
        {
            // Get the type
            Type type = value.GetType();

            // Get field info for this type
            FieldInfo? fieldInfo = type.GetField(value.ToString());

            // Get the string value attributes
            StringValueAttribute[]? attributes = fieldInfo?.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            // Return the first if there was a match.
            return attributes?.Length > 0 ? attributes[0].Value : null;
        }
    }
}
