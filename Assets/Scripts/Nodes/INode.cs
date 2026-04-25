using Core.Links;
using System.Collections.Generic;

namespace Core.Nodes
{
    public interface INode<T> : IView where T : INode<T>
    {
        public List<ILink<T>> Links { get; }
    }
}
