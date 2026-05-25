using ThisProject.Inputs;
using ThisProject.Nodes;
using ThisProject.Views;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ThisProject.Fields.Grids
{
    public class GridFieldClickHandler<T, V> : IClickHandler
        where T : class, INode<Vector2Int>
        where V : class, IView<Vector2Int>
    {
        private Action<T, PointerEventData.InputButton, InputSnapshot> _nodeClickedCallback;
        private Action<PointerEventData.InputButton, InputSnapshot> _fieldClickedCallback;

        private readonly VisibleGridField<T, V> _field;
        private readonly IInputService _inputService;


        public GridFieldClickHandler(VisibleGridField<T, V> field, IInputService inputService)
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
            Vector3 localPos = _field.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);

            int x = Mathf.FloorToInt(localPos.x / _field.Grid.cellSize.x);
            int y = Mathf.FloorToInt(localPos.y / _field.Grid.cellSize.y);

            var node = _field.GetNodeById(new Vector2Int(x, y));
            if (node != null)
            {
                _nodeClickedCallback?.Invoke(node, eventData.button, _inputService.CreateSnapshot());
                return;
            }

            _fieldClickedCallback.Invoke(eventData.button, _inputService.CreateSnapshot());
        }
    }
}