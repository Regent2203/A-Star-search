using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links
{
    public interface ILinksProvider<T, TId> where T : class, INode<T, TId>
    {
        public IEnumerable<ILink<T, TId>> GetLinks(T from, IEnumerable<T> neighbours);
    }
}
