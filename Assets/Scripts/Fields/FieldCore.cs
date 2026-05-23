using Core.Nodes;
using Core.ObjectsStorages;
using System;

namespace Core.Fields
{
    public abstract class FieldCore<T, TId> : IField<T, TId>
        where T : class, INode<TId>
    {
        public abstract IObjectsStorage<T, TId> Nodes { get; }
        public T GetNodeById(TId id) => Nodes.GetById(id);

        public event Action FieldChanged;
        //public event Action NodeMoved;  ???


        public void NotifyFieldChanged() //public?
        {
            FieldChanged?.Invoke();
        }
    }
}