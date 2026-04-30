using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsPainter
    {
        private CellsConfig _cellsConfig;

        public CellsPainter(CellsConfig cellsConfig)
        {
            _cellsConfig = cellsConfig;
        }

        public void TryChangeCellType(Cell cell)
        {
            CellType cellType = null;
            
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftAlt))
                {
                    cellType = _cellsConfig.GetCellType(CellId.Swamp);
                }
                else if (Input.GetKey(KeyCode.LeftControl))
                {
                    cellType = _cellsConfig.GetCellType(CellId.Sand);
                }
                else if (Input.GetKey(KeyCode.LeftAlt))
                {
                    cellType = _cellsConfig.GetCellType(CellId.Dirt);
                }
                else
                {
                    if (Input.GetMouseButton(0)) //lmb
                    {
                        cellType = _cellsConfig.GetCellType(CellId.Obstacle);
                    }
                    else if (Input.GetMouseButton(1)) //rmb
                    {
                        cellType = _cellsConfig.GetCellType(CellId.Normal);
                    }
                }
            }

            if (cellType != null)
                cell.ChangeType(cellType);
        }
    }
}