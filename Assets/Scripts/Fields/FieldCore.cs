using Core.Nodes;
using Core.ObjectsStorages;
using System;
using UnityEngine;

namespace Core.Fields.Grids
{
    public abstract class FieldCore<T, TId> : IField<T, TId> where T : class, INode<TId>
    {
        public abstract IObjectsStorage<T, TId> Nodes { get; }
        public T GetNodeById(TId id) => Nodes.GetById(id);

        public event Action FieldChanged;
        

        public void NotifyFieldChanged()
        {
            FieldChanged?.Invoke();
        }
    }
}