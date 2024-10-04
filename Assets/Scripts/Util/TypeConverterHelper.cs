using System.ComponentModel;

namespace Script.Util
{
    public static class TypeConverterHelper
    {
        public static T? GetConvertingData<T>(string data) where T : struct
        {
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
            if (converter != null && converter.IsValid(data))
                return (T)converter.ConvertFromString(data);
            return null;
        }
    }
}