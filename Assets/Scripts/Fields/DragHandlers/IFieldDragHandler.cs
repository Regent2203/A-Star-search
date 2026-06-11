using System;
using ThisProject.Inputs;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisProject.Fields.DragHandlers
{
    public interface IFieldDragHandler<V> : IBeginDragHandler, IDragHandler, IEndDragHandler
        where V : IView
    {
        public event Action<V, Vector2, PointerEventData.InputButton, InputSnapshot> ViewDragStarted;
        public event Action<V, Vector2, PointerEventData.InputButton, InputSnapshot> ViewDragging;
        public event Action<V, Vector2, PointerEventData.InputButton, InputSnapshot> ViewDragEnded;
    }
}