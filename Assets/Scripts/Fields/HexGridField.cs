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
        Even = 0,
        Odd = 1,        
    }

    public class HexGridField : GridField
    {
        [SerializeField]
        private HexOffsetType _hexOffsetType;        


        public override void SetSize(Vector2Int size)
        {
            _size = size;

            Vector2 calculatedSize = Vector2.zero;
            HexOrientationType orientation = GetHexOrientationType();

            if (orientation == HexOrientationType.PointyTopped)
            {
                calculatedSize = _grid.cellSize * new Vector2(_size.x + 0.5f, _size.y * 0.75f + 0.25f);
            }
            else if (orientation == HexOrientationType.FlatTopped)
            {
                calculatedSize = _grid.cellSize * new Vector2(_size.x * 0.75f + 0.25f, _size.y + 0.5f); //todo check
            }

            _collider.size = calculatedSize;
            _collider.offset = new Vector2(_grid.cellSize.x / 4, 0);
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