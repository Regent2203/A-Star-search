using ThisProject.Nodes;
using Zenject;

namespace ThisProject.Links
{
    public class LinkViewPool<TId> : MonoPoolableMemoryPool<INodeView<TId>, INodeView<TId>, PlacementType, LinkView<TId>>
    {        
    }
}