using System;
using System.Collections.Generic;

namespace Core.Implementations.Cells
{
    public enum BrushType
    {
        Primary,
        Secondary,
    }

    public class CellsPainter
    {
        private readonly Dictionary<BrushType, CellType> _brushes = new Dictionary<BrushType, CellType>();

        public event Action<BrushType, CellType> BrushChanged;


        public void SetBrush(BrushType brush, CellType cellType)
        {
            _brushes[brush] = cellType;
            BrushChanged?.Invoke(brush, cellType);
        }

        public void TryChangeCellType(CellNode node, BrushType brush)
        {
            if (_brushes.TryGetValue(brush, out CellType cellType))
            {
                if (cellType != null)
                {
                    node.ChangeType(cellType);
                }
            }
        }
    }
}