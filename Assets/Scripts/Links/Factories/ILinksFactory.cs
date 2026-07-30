using System.Collections.Generic;
using ThisProject.Nodes;

namespace ThisProject.Links.Factories
{
    public interface ILinksFactory<T, TId>
        where T : INodeData<TId>
    {
        public IEnumerable<ILinkData<TId>> CreateLinksFromNode(T from, IEnumerable<T> neighbours);
        public IEnumerable<ILinkData<TId>> CreateLinksToNode(T to, IEnumerable<T> neighbours);
        public ILinkData<TId> CreateLink(T from, T to);
    }
}