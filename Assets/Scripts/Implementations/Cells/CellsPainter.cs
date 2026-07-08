using System;
using System.Collections.Generic;

namespace ThisProject.Implementations.Cells
{
    public enum BrushType
    {
        Primary,
        Secondary,
    }

    public class CellsPainter
    {
        private readonly Dictionary<BrushType, CellType> _brushes = new Dictionary<BrushType, CellType>();
        
        private readonly ICellTypeChanger _cellTypeChanger;

        public event Action<BrushType, CellType> BrushChanged;


        public CellsPainter(ICellTypeChanger cellChanger)
        {
            _cellTypeChanger = cellChanger;
        }

        public void SetBrush(BrushType brush, CellType cellType)
        {
            _brushes[brush] = cellType;
            BrushChanged?.Invoke(brush, cellType);
        }

        public void PaintCell(CellData node, BrushType brush)
        {
            if (_brushes.TryGetValue(brush, out CellType cellType))
            {
                _cellTypeChanger.TryChangeCellType(node, cellType);
            }
        }
    }
}