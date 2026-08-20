using UnityEngine;

namespace EasyField.Fields
{
    public class RectGridField : GridField
    {
        public override void SetSize(Vector2Int size)
        {
            _size = size;

            _collider.size = _grid.cellSize * new Vector2(_size.x, _size.y);
        }

        public Vector2Int PositionToIndex(Vector2 coords)
        {
            var localPos = transform.InverseTransformPoint(coords);

            int x = Mathf.FloorToInt(localPos.x / _grid.cellSize.x + _size.x / 2f);
            int y = Mathf.FloorToInt(localPos.y / _grid.cellSize.y + _size.y / 2f);
            
            return new Vector2Int(x, y);
        }
    }
}