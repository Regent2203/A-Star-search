using ThisProject.Nodes;
using System;
using ThisProject.ObjectsStorages;

namespace ThisProject.Fields
{
    public interface ILogicField<T, TId>
        where T : class, INode<TId>
    {
        public IObjectsStorage<T, TId> Nodes { get; }
        public T GetNodeById(TId id);

        public event Action FieldChanged;
    }
}
