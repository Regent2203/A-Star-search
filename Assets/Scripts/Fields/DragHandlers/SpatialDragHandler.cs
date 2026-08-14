using EasyField.Inputs;
using EasyField.Nodes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.Fields.DragHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpatialDragHandler<TNodeView> : MonoBehaviour, IFieldDragHandler<TNodeView>
        where TNodeView : MonoBehaviour, INodeView
    {
        private Camera _mainCamera;
        private IInputService _inputService;

        private PointerEventData.InputButton? _currentBtn = null;
        private TNodeView _currentView;
        private Vector2 _offset;

        public event Action<TNodeView, Vector2, PointerEventData.InputButton, InputSnapshot> NodeViewDragStarted;
        public event Action<TNodeView, Vector2, PointerEventData.InputButton, InputSnapshot> NodeViewDragging;
        public event Action<TNodeView, Vector2, PointerEventData.InputButton, InputSnapshot> NodeViewDragEnded;


        [Inject]
        public void Construct(Camera camera, IInputService inputService)
        {
            _mainCamera = camera;
            _inputService = inputService;
        }

        private void ResetValues()
        {
            _currentBtn = null;
            _currentView = null;
            _offset = Vector2.zero;
        }

        public void CancelDrag()
        {
            ResetValues();
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {            
            if (_currentBtn != null)
                return;

            if (eventData.TryGetHitObject(out var hitObject))
            {
                var nodeView = hitObject.GetComponentInParent<TNodeView>();

                if (nodeView != null)
                {
                    _currentBtn = eventData.button;
                    _currentView = nodeView;

                    Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(eventData.position);
                    var startPosition = (Vector2)_currentView.transform.position;
                    _offset = startPosition - mouseWorldPos;

                    NodeViewDragStarted?.Invoke(_currentView, startPosition, eventData.button, _inputService.CreateSnapshot());
                }
            }
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (eventData.button != _currentBtn) 
                return;

            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(eventData.position);
            var targetPosition = mouseWorldPos + _offset;

            NodeViewDragging?.Invoke(_currentView, targetPosition, eventData.button, _inputService.CreateSnapshot());
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (eventData.button != _currentBtn)
                return;

            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(eventData.position);
            var finalPosition = mouseWorldPos + _offset;

            NodeViewDragEnded?.Invoke(_currentView, finalPosition, eventData.button, _inputService.CreateSnapshot());
            
            ResetValues();
        }
    }
}
