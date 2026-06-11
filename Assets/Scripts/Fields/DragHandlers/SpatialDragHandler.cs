using System;
using System.Collections.Generic;
using ThisProject.Inputs;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Fields.DragHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SpatialDragHandler<V> : MonoBehaviour, IFieldDragHandler<V>
        where V : MonoBehaviour, IView
    {
        private Camera _mainCamera;
        private IInputService _inputService;

        private List<PointerEventData.InputButton> _buttons;
        private V _currentView;
        private Vector2 _offset;

        public event Action<V, Vector2, PointerEventData.InputButton, InputSnapshot> ViewDragStarted;
        public event Action<V, Vector2, PointerEventData.InputButton, InputSnapshot> ViewDragging;
        public event Action<V, Vector2, PointerEventData.InputButton, InputSnapshot> ViewDragEnded;


        [Inject]
        public void Construct(Camera camera, IInputService inputService)
        {
            _mainCamera = camera;
            _inputService = inputService;
        }

        private void ResetValues()
        {
            _currentView = null;
            _offset = Vector2.zero;
        }

        public void CancelDrag()
        {
            ResetValues();
        }

        public void SetAllowedInputButtons(params PointerEventData.InputButton[] buttons)
        {
            _buttons = new List<PointerEventData.InputButton>(buttons);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
            if (!_buttons.Contains(eventData.button))
                return;

            var hitObject = eventData.pointerCurrentRaycast.gameObject;

            if (hitObject != null && hitObject.TryGetComponent<V>(out var view))
            {
                _currentView = view;

                Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(eventData.position);
                var startPosition = (Vector2)_currentView.transform.position;
                _offset = startPosition - mouseWorldPos;

                ViewDragStarted?.Invoke(_currentView, startPosition, eventData.button, _inputService.CreateSnapshot());
            }
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            if (!_buttons.Contains(eventData.button))
                return;

            if (_currentView == null) 
                return;

            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(eventData.position);
            var targetPosition = mouseWorldPos + _offset;

            ViewDragging?.Invoke(_currentView, targetPosition, eventData.button, _inputService.CreateSnapshot());
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            if (!_buttons.Contains(eventData.button))
                return;

            if (_currentView == null) 
                return;

            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(eventData.position);
            var finalPosition = mouseWorldPos + _offset;

            ViewDragEnded?.Invoke(_currentView, finalPosition, eventData.button, _inputService.CreateSnapshot());
            
            ResetValues();
        }
    }
}
