namespace ThisProject.SaveSystem.Serializers
{
    public interface IStringSerializer
    {
        public string Serialize<T>(T obj);
        public T Deserialize<T>(string text);
    }
}