using Zenject;

namespace ThisProject.Links
{
    public class LinkDataPool<TId> : PoolableMemoryPool<TId, TId, float, LinkData<TId>>
    {
    }
}