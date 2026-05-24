using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.Links.Providers
{
    public interface ILinksProvider<T> where T : class, INode
    {
        public IEnumerable<ILink<T>> GetLinksForNode(T node);
    }
}
