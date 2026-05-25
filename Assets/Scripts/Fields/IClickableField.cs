using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.Views;
using UnityEngine.EventSystems;

namespace ThisProject.Fields
{
    public interface IClickableField<T, V, TId> : IPointerDownHandler
        where T : class, INode<TId>
        where V : class, IView<TId>
    {
        public abstract IClickHandler ClickHandler { get; }

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<IVisibleField<T, V, TId>, PointerEventData.InputButton, InputSnapshot> FieldClicked;


        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            ClickHandler.ProcessClick(eventData);
        }
    }
}
