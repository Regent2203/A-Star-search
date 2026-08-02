using Zenject;

namespace ThisProject.Links.Implementations
{
    public class LinkViewPool<TId> : MonoPoolableMemoryPool<TId, TId, PlacementType, LinkView<TId>>
    {        
    }
}