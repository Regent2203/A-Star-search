using System;

namespace ThisProject.Implementations.Cells
{
    public interface ICellTypeChanger
    {
        public bool TryChangeCellType(CellNode node, CellType type);

        public event Action<CellNode, CellType> CellTypeChanged;
    }
}