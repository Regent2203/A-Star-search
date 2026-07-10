using Newtonsoft.Json;
using System.IO;
using System.Text;

namespace ThisProject.SaveSystem.Serializers
{
    public class NewtonsoftJsonBinarySerializer : IBinarySerializer
    {
        private readonly JsonSerializer _serializer;
        private readonly Encoding _encoding;


        public NewtonsoftJsonBinarySerializer(JsonSerializerSettings settings = null)
        {
            _serializer = settings != null ? JsonSerializer.Create(settings) : JsonSerializer.CreateDefault();
            _encoding = new UTF8Encoding(false);
        }

        public byte[] Serialize<T>(T obj)
        {
            using var memoryStream = new MemoryStream();
            using var streamWriter = new StreamWriter(memoryStream, _encoding);
            using var jsonWriter = new JsonTextWriter(streamWriter);

            _serializer.Serialize(jsonWriter, obj);

            jsonWriter.Flush();
            streamWriter.Flush();

            return memoryStream.ToArray();
        }

        public T Deserialize<T>(byte[] bytes)
        {
            using var memoryStream = new MemoryStream(bytes);
            using var streamReader = new StreamReader(memoryStream, _encoding);
            using var jsonReader = new JsonTextReader(streamReader);

            return _serializer.Deserialize<T>(jsonReader);
        }
    }
}