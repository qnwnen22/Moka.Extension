using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Moka.Extension
{
    public static class JsonExtension
    {
        public static JToken GetToken(this JObject jObject, params string[] keys)
        {
            JToken jToken = null;
            foreach (string key in keys)
            {
                bool isTry = jObject.TryGetValue(key, out jToken);
                if (isTry == false) throw new Exception($"[{key}]로 이루어진 JToken을 찾을 수 없습니다.");
            }
            return jToken;
        }

        public static JToken GetToken(this JToken jToken, params string[] keys)
        {
            JObject jObject = jToken.ToObject<JObject>();
            return jObject.GetToken(keys);
        }

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
