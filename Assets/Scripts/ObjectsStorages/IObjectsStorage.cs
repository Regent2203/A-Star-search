namespace Core.ObjectsStorages
{
    public interface IObjectsStorage<out T, in TId>
    {
        public T GetById(TId id);
    }
}