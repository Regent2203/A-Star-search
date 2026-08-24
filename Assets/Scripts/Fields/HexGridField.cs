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
                colliderSize = new Vector2(_grid.cellSize.x * (_size.x + 0.5f), _grid.cellSize.y * (_size.y * 0.75f + 0.25f));                

                if (offsetType == HexOffsetType.Odd)
                {
                    colliderOffset = new Vector2(-_grid.cellSize.x / 2, -_grid.cellSize.y / 2);
                    _grid.transform.localPosition = new Vector2(-2.0f * (size.x - 0.5f), -1.5f * (size.y - 1.0f));
                }
                else if (offsetType == HexOffsetType.Even)
                {
                    colliderOffset = new Vector2(-_grid.cellSize.x / 2 - 2.0f, -_grid.cellSize.y / 2);
                    _grid.transform.localPosition = new Vector2(-2.0f * (size.x - 1.5f), -1.5f * (size.y - 1.0f));
                }
            }
            else if (orientation == HexOrientationType.FlatTopped)
            {
                colliderSize = new Vector2(_grid.cellSize.y * (_size.x * 0.75f + 0.25f), _grid.cellSize.x * (_size.y + 0.5f));                

                if (offsetType == HexOffsetType.Odd)
                {
                    colliderOffset = new Vector2(-_grid.cellSize.x / 2, -_grid.cellSize.y / 2);
                    _grid.transform.localPosition = new Vector2(-1.5f * (size.x - 1.0f), -2.0f * (size.y - 0.5f));
                }
                else if (offsetType == HexOffsetType.Even)
                {
                    colliderOffset = new Vector2(-_grid.cellSize.x / 2, -_grid.cellSize.y / 2 - 2.0f);
                    _grid.transform.localPosition = new Vector2(-1.5f * (size.x - 1.0f), -2.0f * (size.y - 1.5f));
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