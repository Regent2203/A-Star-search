using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Fields.ClickHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpatialClickHandler<V> : MonoBehaviour, IFieldClickHandler<V>
        where V : MonoBehaviour, INodeView
    {
        private IInputService _inputService;

        public event Action<V, PointerEventData.InputButton, InputSnapshot> NodeViewClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;


        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            var hitObject = eventData.pointerCurrentRaycast.gameObject;

            if (hitObject != null && hitObject.TryGetComponent<V>(out var view))
            {
                NodeViewClicked?.Invoke(view, eventData.button, _inputService.CreateSnapshot());
                return;
            }

            FieldClicked?.Invoke(eventData.button, _inputService.CreateSnapshot());
        }
    }
}