using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.Links.Providers
{
    public interface ILinksProvider<T>
        where T : INodeData
    {
        public IEnumerable<ILinkData<T>> GetLinksFromNode(T node);
        public IEnumerable<ILinkData<T>> GetLinksToNode(T node);
    }
}
