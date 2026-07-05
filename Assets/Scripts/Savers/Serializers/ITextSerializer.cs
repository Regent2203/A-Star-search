namespace ThisProject.Savers.Serializers
{
    public interface ITextSerializer
    {
        public string Serialize<T>(T obj);
        public T Deserialize<T>(string text);
    }
}