using Core.Inputs;
using Core.Nodes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Fields.Grids
{
    public class GridClickHandler<T> where T : class, INode<Vector2Int>
    {
        private GridFieldBase<T> _field;
        private Grid _grid;
        private Action<T, PointerEventData.InputButton, InputSnapshot> _onClickCallback;

        private readonly IInputService _inputService;


        public GridClickHandler(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void SetConfiguration(GridFieldBase<T> field, Grid grid, Action<T, PointerEventData.InputButton, InputSnapshot> onClickCallback)
        {
            _field = field;
            _grid = grid;
            _onClickCallback = onClickCallback;
        }

        public void ProcessClick(PointerEventData eventData)
        {
            Vector3 localPos = _field.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);

            int x = Mathf.FloorToInt(localPos.x / _grid.cellSize.x);
            int y = Mathf.FloorToInt(localPos.y / _grid.cellSize.y);

            var node = _field.GetNodeById(new Vector2Int(x, y));
            if (node != null)
            {
                _onClickCallback?.Invoke(node, eventData.button, _inputService.CreateSnapshot());
            }
        }
    }
}