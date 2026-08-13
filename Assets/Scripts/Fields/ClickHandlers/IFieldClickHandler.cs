using EasyField.Inputs;
using EasyField.Links;
using EasyField.Nodes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EasyField.Fields.ClickHandlers
{
    public interface IFieldClickHandler<TNodeView> : IPointerDownHandler
        where TNodeView : MonoBehaviour, INodeView
    {
        public event Action<TNodeView, PointerEventData.InputButton, InputSnapshot> NodeViewClicked;
        public event Action<Vector2, PointerEventData.InputButton, InputSnapshot> FieldClicked;
    }

    public interface IFieldClickHandler<TNodeView, TLinkView> : IFieldClickHandler<TNodeView>
        where TNodeView : MonoBehaviour, INodeView
        where TLinkView : MonoBehaviour, ILinkView
    {
        public event Action<TLinkView, PointerEventData.InputButton, InputSnapshot> LinkViewClicked;
    }
}