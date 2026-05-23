using Core.Inputs;
using Core.Nodes;
using Core.ObjectsStorages;
using Core.Views;
using System;
using UnityEngine.EventSystems;
using UnityEngine;


namespace Core.Fields
{
    public abstract class VisualField<T, V, TId> : MonoBehaviour, IPointerDownHandler, IVisualField<T, V, TId>
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        protected abstract IField<T, TId> Core { get; }
        protected abstract IClickHandler ClickHandler { get; }
        protected V _viewPrefab;

        public IObjectsStorage<T, TId> Nodes => Core.Nodes;
        public abstract IObjectsStorage<V, TId> Views { get; }

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<IVisualField<T, V, TId>, PointerEventData.InputButton, InputSnapshot> FieldClicked;
        public event Action FieldChanged
        {
            add => Core.FieldChanged += value;
            remove => Core.FieldChanged -= value;
        }


        public T GetNodeById(TId id) => Nodes.GetById(id);
        public V GetViewById(TId id) => Views.GetById(id);

        
        public void OnPointerDown(PointerEventData eventData)
        {
            ClickHandler.ProcessClick(eventData);
        }

        protected void NotifyNodeClicked(T node, PointerEventData.InputButton btn, InputSnapshot input)
        {
            NodeClicked?.Invoke(node, btn, input);
        }

        protected void NotifyFieldClicked(PointerEventData.InputButton btn, InputSnapshot input)
        {
            FieldClicked?.Invoke(this, btn, input);
        }
    }
}