using EasyField.Fields;
using EasyField.Fields.ClickHandlers;
using EasyField.Implementations.Cells;
using UnityEngine;
using Zenject;

namespace EasyField.Implementations.Hexes
{
    public class HexesClickHandler : GridFieldClickHandler<CellView>
    {
        private HexOrientationType _hexOrientationType;
        private HexOffsetType _hexOffsetType;

        [Inject]
        public void Construct(HexGridField field)
        {
            _hexOrientationType = field.GetHexOrientationType();
            _hexOffsetType = field.GetHexOffsetType();
        }

        protected override void CorrectIndexByGridType(ref Vector2Int index)
        {
            if (_hexOrientationType == HexOrientationType.FlatTopped)
            {
                index = new Vector2Int(index.y, index.x);

                if (_hexOffsetType == HexOffsetType.Even)
                    if ((index.x & 1) == 1)
                        index.y += 1;
            }
            else if (_hexOrientationType == HexOrientationType.PointyTopped)
            {
                if (_hexOffsetType == HexOffsetType.Even)
                    if ((index.y & 1) == 1)
                        index.x += 1;
            }

            Debug.Log($"Cell clicked: {index}");
        }
    }
}