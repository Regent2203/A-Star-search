using Newtonsoft.Json;

namespace ThisProject.Savers.Serializers
{
    public class NewtonsoftJsonTextSerializer : ITextSerializer
    {
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings()
        {
            Formatting = Formatting.Indented,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };


        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj, _settings);
        }

        public T Deserialize<T>(string rawData)
        {
            return JsonConvert.DeserializeObject<T>(rawData, _settings);
        }
    }
}
