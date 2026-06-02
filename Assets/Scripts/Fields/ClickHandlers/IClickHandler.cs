using System;
using ThisProject.Inputs;
using ThisProject.Views;
using UnityEngine.EventSystems;

namespace ThisProject.Fields.ClickHandlers
{
    public interface IClickHandler<V> : IPointerDownHandler
        where V : IView
    {
        public event Action<V, PointerEventData.InputButton, InputSnapshot> ViewClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;
    }
}