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
        Even = 1,
        Odd = 0,        
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

            HexOrientationType orientation = GetHexOrientationType();

            if (orientation == HexOrientationType.PointyTopped)
            {
                colliderSize = new Vector2(_grid.cellSize.x * (_size.x + 0.5f), _grid.cellSize.y * (_size.y * 0.75f + 0.25f));
                colliderOffset = new Vector2(_grid.cellSize.x / 4, 0);
            }
            else if (orientation == HexOrientationType.FlatTopped)
            {
                colliderSize = new Vector2(_grid.cellSize.y * (_size.x * 0.75f + 0.25f), _grid.cellSize.x * (_size.y + 0.5f));
                colliderOffset = new Vector2(0, -_grid.cellSize.x / 4);
            }

            _collider.size = colliderSize;
            _collider.offset = colliderOffset;
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