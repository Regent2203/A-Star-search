using UnityEngine;

namespace ThisProject.SaveSystem.Serializers
{
    public class UnityJsonTextSerializer : ITextSerializer
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