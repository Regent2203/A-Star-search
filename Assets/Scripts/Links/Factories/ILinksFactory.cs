using System.Collections.Generic;
using ThisProject.Nodes;

namespace ThisProject.Links.Factories
{
    public interface ILinksFactory<T, L, TId>
        where T : INodeData<TId>
        where L : ILinkData<TId>
    {
        public IEnumerable<L> CreateLinksFromNode(T from, IEnumerable<T> neighbours);
        public IEnumerable<L> CreateLinksToNode(T to, IEnumerable<T> neighbours);
        public L CreateLink(T from, T to);
    }
}