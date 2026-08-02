using Zenject;

namespace ThisProject.Links.Implementations
{
    public class LinkDataPool<TId> : PoolableMemoryPool<TId, TId, float, LinkData<TId>>
    {
    }
}