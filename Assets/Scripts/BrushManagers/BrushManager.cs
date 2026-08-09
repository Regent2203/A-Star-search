using System;
using System.Collections.Generic;

namespace EasyField.BrushManagers
{
    public class BrushManager<TBrushType> : IBrushManager<TBrushType>
    {
        private readonly Dictionary<int, TBrushType> _brushes = new();

        public event Action<int, TBrushType> BrushChanged;


        /*
        public void PaintCell(CellData nodeData, int brushIndex)
        {
            if (_brushes.TryGetValue(brush, out CellType cellType))
            {
                _cellTypeChanger.TryChangeCellType(nodeData, cellType);
            }
        }*/

        public TBrushType GetBrush(int brushIndex)
        {
            return _brushes[brushIndex];
        }

        public void SetBrush(int brushIndex, TBrushType cellType)
        {
            _brushes[brushIndex] = cellType;
            BrushChanged?.Invoke(brushIndex, cellType);
        }
    }
}