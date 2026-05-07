using Core.Links;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields
{
    public interface IField<T> where T : INode<T>
    {
        public IEnumerable<ILink<T>> GetLinksForNode(T node);
    }
}