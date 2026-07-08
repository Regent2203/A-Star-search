using System;

namespace ThisProject.Implementations.Cells
{
    public class CellTypeChanger : ICellTypeChanger
    {
        public event Action<CellData, CellType> CellTypeChanged;


        public bool TryChangeCellType(CellData node, CellType cellType)
        {
            if (node == null || cellType == null)
                return false;

            if (node.TryChangeCellType(cellType))
            {
                CellTypeChanged?.Invoke(node, cellType);
                return true;
            }

            return false;
        }
    }
}