using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields
{
    public interface IField<T> where T : INode<T>
    {
        public IReadOnlyList<T> Nodes { get; }
    }
}