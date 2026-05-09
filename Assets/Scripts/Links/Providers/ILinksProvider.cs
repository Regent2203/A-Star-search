using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links.Providers
{
    public interface ILinksProvider<T, TId> where T : class, INode<T, TId>
    {
        public IEnumerable<ILink<T, TId>> GetLinksForNode(T node);
    }
}
