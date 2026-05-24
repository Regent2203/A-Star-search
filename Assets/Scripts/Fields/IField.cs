using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using System;

namespace ThisProject.Fields
{
    public interface IField<T, TId> 
        where T : class, INode<TId>
    {
        public IObjectsStorage<T, TId> Nodes { get; }
        public T GetNodeById(TId id);

        public event Action FieldChanged;
    }
}
