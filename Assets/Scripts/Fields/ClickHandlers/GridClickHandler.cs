using System;
using ThisProject.Fields.Implementations;
using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisProject.Fields.ClickHandlers
{
    public class GridClickHandler<T, V> : IClickHandler
        where T : class, INode<Vector2Int>
        where V : class, IView<Vector2Int>
    {
        private Action<T, PointerEventData.InputButton, InputSnapshot> _nodeClickedCallback;
        private Action<PointerEventData.InputButton, InputSnapshot> _fieldClickedCallback;

        private readonly GridSceneField<T, V> _field;
        private readonly IInputService _inputService;


        public GridClickHandler(GridSceneField<T, V> field, IInputService inputService)
        {
            _field = field;
            _inputService = inputService;
        }

        public void SetConfiguration(Action<T, PointerEventData.InputButton, InputSnapshot> nodeClickedCallback,
             Action<PointerEventData.InputButton, InputSnapshot> fieldClickedCallback)
        {
            _nodeClickedCallback = nodeClickedCallback;
            _fieldClickedCallback = fieldClickedCallback;
        }

        public void ProcessClick(PointerEventData eventData)
        {
            var index = _field.PositionToIndex(eventData.pointerCurrentRaycast.worldPosition);

            var node = _field.GetNodeById(index);
            if (node != null)
            {
                _nodeClickedCallback?.Invoke(node, eventData.button, _inputService.CreateSnapshot());
                return;
            }

            _fieldClickedCallback.Invoke(eventData.button, _inputService.CreateSnapshot());
        }
    }
}