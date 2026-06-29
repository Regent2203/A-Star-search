using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisProject.Fields.ClickHandlers
{
    public interface IFieldClickHandler<V> : IPointerDownHandler
        where V : MonoBehaviour, INodeView
    {
        public event Action<V, PointerEventData.InputButton, InputSnapshot> NodeViewClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;
    }
}