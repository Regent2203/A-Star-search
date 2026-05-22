using Core.Inputs;
using Core.Nodes;
using Core.ObjectsStorages;
using Core.Views;
using System;
using UnityEngine.EventSystems;

namespace Core.Fields
{
    public interface IVisualField<T, V, TId> : IField<T, TId> 
        where T : class, INode<TId> 
        where V : class, IView
    {
        public IObjectsStorage<V, TId> Views { get; }

        public V GetViewById(TId id) => Views.GetById(id);

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
    }
}
