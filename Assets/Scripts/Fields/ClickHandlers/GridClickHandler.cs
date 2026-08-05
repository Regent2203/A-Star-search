using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Fields.ClickHandlers
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GridClickHandler<TNodeView> : MonoBehaviour, IFieldClickHandler<TNodeView> 
        where TNodeView : MonoBehaviour, INodeView<Vector2Int>
    {
        private GridField _field;
        private GridTypeStorage<TNodeView> _views;
        private Camera _mainCamera;
        private IInputService _inputService;

        public event Action<TNodeView, PointerEventData.InputButton, InputSnapshot> NodeViewClicked;
        public event Action<Vector2, PointerEventData.InputButton, InputSnapshot> FieldClicked;


        [Inject]
        public void Construct(GridField field, GridTypeStorage<TNodeView> views, Camera camera, IInputService inputService)
        {
            _field = field;
            _views = views;
            _mainCamera = camera;
            _inputService = inputService;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            var index = _field.PositionToIndex(eventData.pointerCurrentRaycast.worldPosition);

            var view = _views.GetItem(index);
            if (view != null)
            {
                NodeViewClicked?.Invoke(view, eventData.button, _inputService.CreateSnapshot());
                return;
            }

            Vector2 worldPos = Camera.main.ScreenToWorldPoint(eventData.position);
            FieldClicked?.Invoke(worldPos, eventData.button, _inputService.CreateSnapshot());
        }
    }
}