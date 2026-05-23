using Core.Nodes;
using Core.ObjectsStorages;
using System;

namespace Core.Fields
{
    public interface IField<T, TId> 
        where T : class, INode<TId>
    {
        public IObjectsStorage<T, TId> Nodes { get; }
        public T GetNodeById(TId id);

        public event Action FieldChanged;
    }
}
