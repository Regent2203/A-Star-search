using System;
using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisProject.Fields.ClickHandlers
{
    public class GridClickHandler<T, V> : IClickHandler<T>
        where T : class, INode<Vector2Int>
        where V : class, IView<Vector2Int>
    {
        private readonly GridField<T, V> _field;
        private readonly IInputService _inputService;

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action<PointerEventData.InputButton, InputSnapshot> FieldClicked;


        public GridClickHandler(GridField<T, V> field, IInputService inputService)
        {
            _field = field;
            _inputService = inputService;
        }

        public void ProcessClick(PointerEventData eventData)
        {
            var index = _field.PositionToIndex(eventData.pointerCurrentRaycast.worldPosition);

            var node = _field.GetNodeById(index);
            if (node != null)
            {
                NodeClicked?.Invoke(node, eventData.button, _inputService.CreateSnapshot());
                return;
            }

            FieldClicked.Invoke(eventData.button, _inputService.CreateSnapshot());
        }
    }
}