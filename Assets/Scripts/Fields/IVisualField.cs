using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using System;
using UnityEngine.EventSystems;

namespace ThisProject.Fields
{
    public interface IVisualField<T, V, TId> : IField<T, TId> 
        where T : class, INode<TId> 
        where V : class, IView<TId>
    {
        public IObjectsStorage<V, TId> Views { get; }

        public V GetViewById(TId id);

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<IVisualField<T, V, TId>, PointerEventData.InputButton, InputSnapshot> FieldClicked;
    }
}
