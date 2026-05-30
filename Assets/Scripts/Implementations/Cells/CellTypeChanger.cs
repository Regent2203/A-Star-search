using System;

namespace ThisProject.Implementations.Cells
{
    public class CellTypeChanger : ICellTypeChanger
    {
        public event Action<CellNode, CellType> CellTypeChanged;


        public bool TryChangeCellType(CellNode node, CellType cellType)
        {
            if (node == null || cellType == null)
                return false;

            if (node.TryChangeType(cellType))
            {
                CellTypeChanged?.Invoke(node, cellType);
                return true;
            }

            return false;
        }
    }
}