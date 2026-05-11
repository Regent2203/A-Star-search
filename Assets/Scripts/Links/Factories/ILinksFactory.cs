using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links.Factories
{
    public interface ILinksFactory<T> where T : class, INode
    {
        public IEnumerable<ILink<T>> CreateLinks(T from, IEnumerable<T> neighbours);
        public ILink<T> CreateLink(T from, T to);
    }
}