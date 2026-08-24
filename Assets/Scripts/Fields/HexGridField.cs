using UnityEngine;

namespace EasyField.Fields
{
    public enum HexOrientationType
    {
        Undefined = 0,
        PointyTopped = 1,
        FlatTopped = 2,
    }

    public enum HexOffsetType
    {
        Odd = 0,
        Even = 1,        
    }

    public class HexGridField : GridField
    {
        [SerializeField]
        private HexOffsetType _hexOffsetType;        


        public override void SetSize(Vector2Int size)
        {
            _size = size;

            Vector2 colliderSize = Vector2.zero;
            Vector2 colliderOffset = Vector2.zero;

            var orientation = GetHexOrientationType();
            var offsetType = GetHexOffsetType();

            if (orientation == HexOrientationType.PointyTopped)
            {
                float cellX = _grid.cellSize.x;
                float cellY = _grid.cellSize.y;
                float stepX = cellX * 0.5f;
                float stepY = cellY * 0.375f;

                colliderSize = new Vector2(cellX * (_size.x + 0.5f), cellY * (_size.y * 0.75f + 0.25f));

                if (offsetType == HexOffsetType.Odd)
                {
                    colliderOffset = new Vector2(-cellX / 2, -cellY / 2);
                    _grid.transform.localPosition = new Vector2(-stepX * (size.x - 0.5f), -stepY * (size.y - 1.0f));
                }
                else if (offsetType == HexOffsetType.Even)
                {
                    colliderOffset = new Vector2(-cellX / 2 - stepX, -cellY / 2);
                    _grid.transform.localPosition = new Vector2(-stepX * (size.x - 1.5f), -stepY * (size.y - 1.0f));
                }
            }
            else if (orientation == HexOrientationType.FlatTopped)
            {
                float cellX = _grid.cellSize.y;
                float cellY = _grid.cellSize.x;

                float stepX = cellX * 0.375f;
                float stepY = cellY * 0.5f;

                colliderSize = new Vector2(cellX * (_size.x * 0.75f + 0.25f), cellY * (_size.y + 0.5f));

                if (offsetType == HexOffsetType.Odd)
                {
                    colliderOffset = new Vector2(-_grid.cellSize.x / 2, -_grid.cellSize.y / 2);
                    _grid.transform.localPosition = new Vector2(-stepX * (size.x - 1.0f), -stepY * (size.y - 0.5f));
                }
                else if (offsetType == HexOffsetType.Even)
                {
                    colliderOffset = new Vector2(-_grid.cellSize.x / 2, -_grid.cellSize.y / 2 - stepY);
                    _grid.transform.localPosition = new Vector2(-stepX * (size.x - 1.0f), -stepY * (size.y - 1.5f));
                }
            }

            _collider.size = colliderSize;
            _collider.offset = colliderOffset + _collider.size / 2;
            UpdateGraphics(colliderSize);
        }

        public HexOffsetType GetHexOffsetType()
        {
            return _hexOffsetType;
        }

        public HexOrientationType GetHexOrientationType()
        {
            if (Grid.cellLayout != GridLayout.CellLayout.Hexagon)
            {
                Debug.LogError($"Incorrect cell layout for HexGridField: {Grid.cellLayout}. 'Hexagon' expected.", this);
                return HexOrientationType.Undefined;
            }
            switch (Grid.cellSwizzle)
            {
                case GridLayout.CellSwizzle.XYZ:
                    return HexOrientationType.PointyTopped;
                case GridLayout.CellSwizzle.YXZ:
                    return HexOrientationType.FlatTopped;
                default:
                    Debug.LogError($"Incorrect cell swizzle type in grid: {Grid.cellSwizzle}. 'XYZ' or 'YXZ' expected.", this);
                    return HexOrientationType.Undefined;
            }
        }
    }
}