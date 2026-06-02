using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Fields.ClickHandlers
{
    public class GridClickHandler<T, V> : MonoBehaviour, IClickHandler<V>
        where T : class, INode<Vector2Int>
        where V : class, IView<Vector2Int>
    {
        private GridField<T, V> _field;
        private IInputService _inputService;

        public event Action<V, PointerEventData.InputButton, InputSnapshot> ViewClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;


        [Inject]
        public void Construct(GridField<T, V> field, IInputService inputService)
        {
            _field = field;
            _inputService = inputService;
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            var index = _field.PositionToIndex(eventData.pointerCurrentRaycast.worldPosition);

            var view = _field.GetViewById(index);
            if (view != null)
            {
                ViewClicked?.Invoke(view, eventData.button, _inputService.CreateSnapshot());
                return;
            }

            FieldClicked?.Invoke(eventData.button, _inputService.CreateSnapshot());
        }
    }
}