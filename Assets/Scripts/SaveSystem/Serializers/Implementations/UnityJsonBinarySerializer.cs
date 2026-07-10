using System.Text;
using UnityEngine;

namespace ThisProject.SaveSystem.Serializers
{
    public class UnityJsonBinarySerializer : IBinarySerializer
    {
        public byte[] Serialize<T>(T obj)
        {
            string json = JsonUtility.ToJson(obj);
            return Encoding.UTF8.GetBytes(json);
        }

        public T Deserialize<T>(byte[] bytes)
        {
            string json = Encoding.UTF8.GetString(bytes);
            return JsonUtility.FromJson<T>(json);
        }
    }
}