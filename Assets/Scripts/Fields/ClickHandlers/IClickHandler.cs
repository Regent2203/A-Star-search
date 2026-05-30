using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using UnityEngine.EventSystems;

namespace ThisProject.Fields.ClickHandlers
{
    public interface IClickHandler<T> : IPointerDownHandler
        where T : class, INode
    {
        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;
    }
}