using Newtonsoft.Json;

namespace Moka.Extension
{
    public static class JsonExtension
    {
        public static string ToJson(this object value, Formatting formatting = Formatting.None)
        {
            return JsonConvert.SerializeObject(value, formatting);
        }

        public static T ToClass<T>(this string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static T DeepCopy<T>(this T t)
        {
            return t.ToJson().ToClass<T>();
        }
    }
}
