using UnityEngine;

namespace EasyField.Serializers
{
    public class JsonUtilityStringSerializer : IStringSerializer
    {
        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj, true);
        }

        public T Deserialize<T>(string text)
        {
            return JsonUtility.FromJson<T>(text);
        }
    }
}