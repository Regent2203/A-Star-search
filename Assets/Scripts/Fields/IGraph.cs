using Core.Links;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields
{
    public interface IGraph<T, TId> where T : class, INode<T, TId>
    {
        public T GetNodeById(TId id);
    }
}