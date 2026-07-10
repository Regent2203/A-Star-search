namespace ThisProject.SaveSystem.Serializers
{
    public interface ISerializer<TFormat>
    {
        public TFormat Serialize<T>(T obj);
        public T Deserialize<T>(TFormat data);
    }
}