using System.Collections.Generic;
using ThisProject.Nodes;

namespace ThisProject.Links.Providers
{
    public interface ILinksProvider<T, L, TId>
        where T : INodeData<TId>
        where L : ILinkData<TId>
    {
        public IEnumerable<L> GetLinksFromNode(T node);
        public IEnumerable<L> GetLinksToNode(T node);
    }
}
