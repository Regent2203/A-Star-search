using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.Links.Factories
{
    public interface ILinksFactory<T> where T : class, INode
    {
        public IEnumerable<ILink<T>> CreateLinksFromNode(T from, IEnumerable<T> neighbours);
        public IEnumerable<ILink<T>> CreateLinksToNode(T to, IEnumerable<T> neighbours);
        public ILink<T> CreateLink(T from, T to);
    }
}