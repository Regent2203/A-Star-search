using Core.Inputs;
using Core.Nodes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Fields.Grids
{
    public class GridFieldClickHandler<T> where T : class, INode<Vector2Int> //todo type + TId
    {
        private GridFieldBase<T> _field;
        private Action<T, PointerEventData.InputButton, InputSnapshot> _nodeClickedCallback;

        private readonly IInputService _inputService;


        public GridFieldClickHandler(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void SetConfiguration(GridFieldBase<T> field, Action<T, PointerEventData.InputButton, InputSnapshot> nodeClickedCallback)
        {
            _field = field;
            _nodeClickedCallback = nodeClickedCallback;
        }

        public bool ProcessClick(PointerEventData eventData)
        {
            Vector3 localPos = _field.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);

            int x = Mathf.FloorToInt(localPos.x / _field.Grid.cellSize.x);
            int y = Mathf.FloorToInt(localPos.y / _field.Grid.cellSize.y);

            var node = _field.Nodes.GetById(new Vector2Int(x, y));
            if (node != null)
            {
                _nodeClickedCallback?.Invoke(node, eventData.button, _inputService.CreateSnapshot());
                return true;
            }

            return false;
        }
    }
}