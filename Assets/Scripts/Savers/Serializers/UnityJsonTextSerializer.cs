using UnityEngine;

namespace ThisProject.Savers.Serializers
{
    public class UnityJsonTextSerializer : ITextSerializer
    {
        public string Serialize<T>(T obj)
        {
            return JsonUtility.ToJson(obj);
        }

        public T Deserialize<T>(string text)
        {
            return JsonUtility.FromJson<T>(text);
        }
    }
}
