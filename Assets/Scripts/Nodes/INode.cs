using Core.Links;
using System.Collections.Generic;

namespace Core.Nodes
{
    public interface INode<T> : IEstimatable, IView where T : INode<T>
    {
        public List<ILink<T>> Links { get; }
        public bool IsBlocked { get; }
    }
}
