using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Fields.ClickHandlers
{
    public class GridClickHandler<V> : MonoBehaviour, IFieldClickHandler<V> 
        where V : class, IView<Vector2Int>
    {
        private GridField _field;
        private GridTypeStorage<V> _views;
        private IInputService _inputService;

        public event Action<V, PointerEventData.InputButton, InputSnapshot> ViewClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;


        [Inject]
        public void Construct(GridField field, GridTypeStorage<V> views, IInputService inputService)
        {
            _field = field;
            _views = views;
            _inputService = inputService;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            var index = _field.PositionToIndex(eventData.pointerCurrentRaycast.worldPosition);

            var view = _views.GetItemById(index);
            if (view != null)
            {
                ViewClicked?.Invoke(view, eventData.button, _inputService.CreateSnapshot());
                return;
            }

            FieldClicked?.Invoke(eventData.button, _inputService.CreateSnapshot());
        }
    }
}