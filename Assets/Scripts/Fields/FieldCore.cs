using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using System;

namespace ThisProject.Fields
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