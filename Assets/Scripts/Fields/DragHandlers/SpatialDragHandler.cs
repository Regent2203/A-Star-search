using System;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisProject.Fields.DragHandlers
{
    public class SpatialDragHandler<V> : MonoBehaviour, IDragHandler<V>
        where V : MonoBehaviour, IView
    {
        private Camera _mainCamera;

        private V _currentView;

        private Vector2 _startPosition; 
        private Vector2 _offset;

        public event Action<V, Vector2, PointerEventData> ViewDragStarted;
        public event Action<V, Vector2, PointerEventData> ViewDragging;
        public event Action<V, Vector2, Vector2, PointerEventData> ViewDragEnded;


        private void Awake()
        {
            _mainCamera = Camera.main;

            ResetValues();
        }

        private void ResetValues()
        {
            _currentView = null;
            _startPosition = Vector2.zero;
            _offset = Vector2.zero;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var hitObject = eventData.pointerCurrentRaycast.gameObject;

            if (hitObject != null && hitObject.TryGetComponent<V>(out var view))
            {
                _currentView = view;

                Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(eventData.position);

                _startPosition = _currentView.transform.position;
                _offset = _startPosition - mouseWorldPos;

                ViewDragStarted?.Invoke(_currentView, _startPosition, eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_currentView == null) 
                return;

            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(eventData.position);

            var targetPosition = mouseWorldPos + _offset;

            ViewDragging?.Invoke(_currentView, targetPosition, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_currentView == null) 
                return;

            Vector2 mouseWorldPos = _mainCamera.ScreenToWorldPoint(eventData.position);

            var finalPosition = mouseWorldPos + _offset;

            ViewDragEnded?.Invoke(_currentView, finalPosition, _startPosition, eventData);
            
            ResetValues();
        }
    }
}
