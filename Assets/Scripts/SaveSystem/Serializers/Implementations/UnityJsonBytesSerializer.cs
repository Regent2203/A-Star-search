using System.Text;
using UnityEngine;

namespace EasyField.SaveSystem.Serializers
{
    public class UnityJsonBytesSerializer : IBytesSerializer
    {
        public byte[] Serialize<T>(T obj)
        {
            string json = JsonUtility.ToJson(obj, true);
            return Encoding.UTF8.GetBytes(json);
        }

        public T Deserialize<T>(byte[] bytes)
        {
            string json = Encoding.UTF8.GetString(bytes);
            return JsonUtility.FromJson<T>(json);
        }
    }
}