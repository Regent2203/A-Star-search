using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.Links.Factories
{
    public interface ILinksFactory<T>
        where T : INodeData
    {
        public IEnumerable<ILinkData<T>> CreateLinksFromNode(T from, IEnumerable<T> neighbours);
        public IEnumerable<ILinkData<T>> CreateLinksToNode(T to, IEnumerable<T> neighbours);
        public ILinkData<T> CreateLink(T from, T to);
    }
}