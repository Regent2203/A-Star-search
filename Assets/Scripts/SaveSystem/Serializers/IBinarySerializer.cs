namespace ThisProject.SaveSystem.Serializers
{
    public interface IBinarySerializer
    {
        public byte[] Serialize<T>(T obj);
        public T Deserialize<T>(byte[] bytes);
    }
}