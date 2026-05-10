using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links.Providers
{
    public interface ILinksProvider<T> where T : class, INode
    {
        public IEnumerable<ILink<T>> GetLinksForNode(T node);
    }
}
