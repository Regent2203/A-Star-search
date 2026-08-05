using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Fields.ClickHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpatialClickHandler<TNodeView> : MonoBehaviour, IFieldClickHandler<TNodeView>
        where TNodeView : MonoBehaviour, INodeView
    {
        private Camera _mainCamera;
        private IInputService _inputService;

        public event Action<TNodeView, PointerEventData.InputButton, InputSnapshot> NodeViewClicked;
        public event Action<Vector2, PointerEventData.InputButton, InputSnapshot> FieldClicked;


        [Inject]
        public void Construct(Camera camera, IInputService inputService)
        {
            _mainCamera = camera;
            _inputService = inputService;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            var hitObject = eventData.pointerCurrentRaycast.gameObject;

            if (hitObject != null && hitObject.TryGetComponent<TNodeView>(out var view))
            {
                NodeViewClicked?.Invoke(view, eventData.button, _inputService.CreateSnapshot());
                return;
            }

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            FieldClicked?.Invoke(worldPos, eventData.button, _inputService.CreateSnapshot());
        }
    }
}