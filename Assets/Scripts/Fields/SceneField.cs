using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.Views;
using System;
using UnityEngine.EventSystems;
using UnityEngine;
using ThisProject.Fields.ClickHandlers;

namespace ThisProject.Fields
{
    public abstract class SceneField<T, V, TId> : MonoBehaviour, IVisibleField<T, V, TId>, IClickableField<T, V, TId>
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        public abstract IFieldCore<T, TId> Core { get; }
        public abstract IFieldVisual<V, TId> Visual { get; }
        public abstract IClickHandler ClickHandler { get; }

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<IVisibleField<T, V, TId>, PointerEventData.InputButton, InputSnapshot> FieldClicked;
        public event Action FieldChanged;

        public V GetViewById(TId id) => Visual.Views.GetById(id);
        public T GetNodeById(TId id) => Core.Nodes.GetById(id);


        protected void NotifyNodeClicked(T node, PointerEventData.InputButton btn, InputSnapshot input)
        {
            NodeClicked?.Invoke(node, btn, input);
        }

        protected void NotifyFieldClicked(PointerEventData.InputButton btn, InputSnapshot input)
        {
            FieldClicked?.Invoke(this, btn, input);
        }

        protected void NotifyFieldChanged()
        {
            FieldChanged?.Invoke();
        }
    }
}