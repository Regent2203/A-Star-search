using UnityEngine;

namespace EasyField.Fields
{
    public class HexGridField : GridField
    {
        public override void SetSize(Vector2Int size)
        {
            _size = size;

            //todo
            _collider.size = _grid.cellSize * new Vector2(_size.x, _size.y);
        }
    }
}