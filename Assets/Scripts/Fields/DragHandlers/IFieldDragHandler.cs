using EasyField.Inputs;
using EasyField.Nodes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyField.Fields.DragHandlers
{
    public interface IFieldDragHandler<TNodeView> : IBeginDragHandler, IDragHandler, IEndDragHandler
        where TNodeView : MonoBehaviour, INodeView
    {
        public event Action<TNodeView, Vector2, PointerEventData.InputButton, InputSnapshot> NodeViewDragStarted;
        public event Action<TNodeView, Vector2, PointerEventData.InputButton, InputSnapshot> NodeViewDragging;
        public event Action<TNodeView, Vector2, PointerEventData.InputButton, InputSnapshot> NodeViewDragEnded;
    }
}