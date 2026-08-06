using Zenject;

namespace EasyField.Links.Implementations
{
    public class LinkDataPool<TId> : PoolableMemoryPool<TId, TId, float, LinkData<TId>>
    {
    }
}