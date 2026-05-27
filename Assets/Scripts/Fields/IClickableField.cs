using System;
using ThisProject.Fields.ClickHandlers;
using ThisProject.Inputs;
using ThisProject.Nodes;
using UnityEngine.EventSystems;

namespace ThisProject.Fields
{
    public interface IClickableField<T> : IPointerDownHandler
        where T : class, INode
    {
        public IClickHandler ClickHandler { get; }

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;


        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            ClickHandler.ProcessClick(eventData);
        }
    }
}
