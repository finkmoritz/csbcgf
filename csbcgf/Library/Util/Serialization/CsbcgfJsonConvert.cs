using System;
using Newtonsoft.Json;

namespace csbcgf
{
    public static class CsbcgfJsonConvert
    {
        public static readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            Converters = new[] { new StackConverter() },
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };

        public static string Serialize(Object obj)
        {
            return JsonConvert.SerializeObject(obj, serializerSettings);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json, serializerSettings);
        }
    }
}
