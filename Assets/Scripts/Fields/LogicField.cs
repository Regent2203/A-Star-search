using System;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;

namespace ThisProject.Fields
{
    public abstract class LogicField<T, TId> : ILogicField<T, TId> 
        where T : class, INode<TId>
    {
        public abstract IObjectsStorage<T, TId> Nodes { get; }

        public event Action FieldChanged;

        public T GetNodeById(TId id) => Nodes.GetById(id);


        protected void NotifyFieldChanged()
        {
            FieldChanged?.Invoke();
        }
    }
}