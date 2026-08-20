using UnityEngine;

namespace EasyField.Fields
{
    public class HexGridField : GridField
    {
        public override void SetSize(Vector2Int size)
        {
            _size = size;

            _collider.size = _grid.cellSize * new Vector2(_size.x + 0.5f, _size.y * 0.75f + 0.25f);
            _collider.offset = new Vector2(_grid.cellSize.x / 4, 0);
        }
    }
}