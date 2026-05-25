using ThisProject.Nodes;
using System;

namespace ThisProject.Fields
{
    public interface IField<T, TId>
        where T : class, INode<TId>
    {
        public IFieldCore<T, TId> Core { get; }
        public T GetNodeById(TId id);

        public event Action FieldChanged;
    }
}
