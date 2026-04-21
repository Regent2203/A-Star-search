using Core.Links;
using System.Collections.Generic;

namespace Core.Nodes
{
    public interface INode<T> : IView
    {
        public List<ILink<T>> Links { get; }
    }
}
