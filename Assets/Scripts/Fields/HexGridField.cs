using UnityEngine;

namespace EasyField.Fields
{
    public enum HexOrientationType
    {
        Undefined = 0,
        PointyTopped = 1,
        FlatTopped = 2,
    }

    public class HexGridField : GridField
    {
        public override void SetSize(Vector2Int size)
        {
            _size = size;

            _collider.size = _grid.cellSize * new Vector2(_size.x + 0.5f, _size.y * 0.75f + 0.25f);
            _collider.offset = new Vector2(_grid.cellSize.x / 4, 0);
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