using Core.Nodes;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Fields.Grids
{
    public class GridInputHandler<T> where T : class, INode<Vector2Int>
    {
        private readonly GridFieldBase<T> _field;
        private readonly Grid _grid;
        private readonly Action<T, PointerEventData.InputButton> _onClickCallback;


        public GridInputHandler(GridFieldBase<T> field, Grid grid, Action<T, PointerEventData.InputButton> onClickAction)
        {
            _field = field;
            _grid = grid;
            _onClickCallback = onClickAction;
        }

        public void ProcessInput(PointerEventData eventData)
        {
            Vector3 localPos = _field.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);

            int x = Mathf.FloorToInt(localPos.x / _grid.cellSize.x);
            int y = Mathf.FloorToInt(localPos.y / _grid.cellSize.y);

            var node = _field.GetNodeById(new Vector2Int(x, y));
            if (node != null)
            {
                _onClickCallback?.Invoke(node, eventData.button);
            }
        }
    }
}