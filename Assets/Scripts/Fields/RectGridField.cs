using UnityEngine;

namespace EasyField.Fields
{
    public class RectGridField : GridField
    {
        public override void SetSize(Vector2Int size)
        {
            _size = size;

            _collider.size = _grid.cellSize * new Vector2(_size.x, _size.y);
            _collider.offset = _collider.size / 2;

            _imgBackground.size = size * (Vector2)_grid.cellSize;
            _imgFrame.size = size * (Vector2)_grid.cellSize + _framePadding;

            transform.position = Vector2.zero - _collider.offset;
        }
    }
}