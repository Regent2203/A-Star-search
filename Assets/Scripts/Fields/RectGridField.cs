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

            _grid.transform.localPosition = -_collider.size / 2;

            UpdateGraphics(_collider.size);
        }
    }
}