using ThisProject.Nodes;
using System;

namespace ThisProject.Fields
{
    public interface IField<T, TId>
        where T : class, INode<TId>
    {
        public abstract IFieldCore<T, TId> Core { get; }

        public event Action FieldChanged;
    }
}
