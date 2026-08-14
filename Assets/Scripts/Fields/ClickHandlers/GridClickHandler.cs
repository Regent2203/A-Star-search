using EasyField.Inputs;
using EasyField.Nodes;
using EasyField.ObjectsStorages;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.Fields.ClickHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GridClickHandler<TNodeView> : MonoBehaviour, IFieldClickHandler<TNodeView> 
        where TNodeView : MonoBehaviour, INodeView<Vector2Int>
    {
        private GridField _field;
        private GridTypeStorage<TNodeView> _nodeViews;
        private Camera _mainCamera;
        private IInputService _inputService;

        public event Action<TNodeView, PointerEventData.InputButton, InputSnapshot> NodeViewClicked;
        public event Action<Vector2, PointerEventData.InputButton, InputSnapshot> FieldClicked;


        [Inject]
        public void Construct(GridField field, GridTypeStorage<TNodeView> nodeViews, Camera camera, IInputService inputService)
        {
            _field = field;
            _nodeViews = nodeViews;
            _mainCamera = camera;
            _inputService = inputService;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            //todo
            var index = _field.PositionToIndex(eventData.pointerCurrentRaycast.worldPosition);
            Debug.Log(index);

            var nodeView = _nodeViews.GetItem(index);
            if (nodeView != null)
            {
                NodeViewClicked?.Invoke(nodeView, eventData.button, _inputService.CreateSnapshot());
                return;
            }

            Vector2 worldPos = _mainCamera.ScreenToWorldPoint(eventData.position);
            FieldClicked?.Invoke(worldPos, eventData.button, _inputService.CreateSnapshot());
        }
    }
}