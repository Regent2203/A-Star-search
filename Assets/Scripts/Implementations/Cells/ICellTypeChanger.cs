using System;

namespace EasyField.Implementations.Cells
{
    public interface ICellTypeChanger
    {
        public bool TryChangeCellType(CellData node, CellType type);

        public event Action<CellData, CellType> CellTypeChanged;
    }
}