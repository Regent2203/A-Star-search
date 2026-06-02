using System;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisProject.Fields.DragHandlers
{
    public interface IDragHandler<V> : IBeginDragHandler, IDragHandler, IEndDragHandler
        where V : IView
    {
        public event Action<V, Vector2, PointerEventData> ViewDragStarted;
        public event Action<V, Vector2, PointerEventData> ViewDragging;
        public event Action<V, Vector2, PointerEventData> ViewDragEnded;
    }
}
