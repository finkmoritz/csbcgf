using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;

namespace Csbcgf.Core
{
    /// <summary>
    /// Converter for any Stack<T> that prevents Json.NET from reversing its order when deserializing.
    /// </summary>
    public class StackConverter : JsonConverter
    {
        // Prevent Json.NET from reversing the order of a Stack<T> when deserializing.
        // https://github.com/JamesNK/Newtonsoft.Json/issues/971
        static Type StackParameterType(Type objectType)
        {
            while (objectType != null)
            {
                if (objectType.IsGenericType)
                {
                    var genericType = objectType.GetGenericTypeDefinition();
                    if (genericType == typeof(Stack<>))
                        return objectType.GetGenericArguments()[0];
                }
                objectType = objectType.BaseType;
            }
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return StackParameterType(objectType) != null;
        }

        object ReadJsonGeneric<T>(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            var list = serializer.Deserialize<List<T>>(reader);
            var stack = existingValue as Stack<T> ?? (Stack<T>)serializer.ContractResolver.ResolveContract(objectType).DefaultCreator();
            for (int i = list.Count - 1; i >= 0; i--)
                stack.Push(list[i]);
            return stack;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;
            try
            {
                var parameterType = StackParameterType(objectType);
                var method = GetType().GetMethod("ReadJsonGeneric", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                var genericMethod = method.MakeGenericMethod(new[] { parameterType });
                return genericMethod.Invoke(this, new object[] { reader, objectType, existingValue, serializer });
            }
            catch (TargetInvocationException ex)
            {
                // Wrap the TargetInvocationException in a JsonSerializerException
                throw new JsonSerializationException("Failed to deserialize " + objectType, ex);
            }
        }

        public override bool CanWrite { get { return false; } }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
