using UnityEngine;

namespace EasyField.SaveSystem.Serializers
{
    public class UnityJsonStringSerializer : IStringSerializer
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