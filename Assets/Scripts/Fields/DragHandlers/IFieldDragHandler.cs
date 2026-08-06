using System;
using EasyField.Inputs;
using EasyField.Nodes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyField.Fields.DragHandlers
{
    public interface IFieldDragHandler<V> : IBeginDragHandler, IDragHandler, IEndDragHandler
        where V : MonoBehaviour, INodeView
    {
        public event Action<V, Vector2, PointerEventData.InputButton, InputSnapshot> NodeViewDragStarted;
        public event Action<V, Vector2, PointerEventData.InputButton, InputSnapshot> NodeViewDragging;
        public event Action<V, Vector2, PointerEventData.InputButton, InputSnapshot> NodeViewDragEnded;
    }
}