using Zenject;

namespace ThisProject.Links.Implementations
{
    public class LinkViewPool<TId> : MonoPoolableMemoryPool<TId, TId, float, PlacementType, LinkView<TId>>
    {        
    }
}