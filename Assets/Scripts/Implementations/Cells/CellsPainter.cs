using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsPainter
    {
        private IInstantiator _instantiator;

        public CellsPainter(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void TryChangeCellType(Cell cell)
        {
            CellType cellType = null;

            if (!Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetMouseButton(0)) //lmb
                {
                    cellType = _instantiator.Instantiate<CellTypeObstacle>();
                }
                else if (Input.GetMouseButton(1)) //rmb
                {
                    cellType = _instantiator.Instantiate<CellTypeNormal>();
                }
            }

            if (cellType != null)
                cell.ChangeType(cellType);
        }
    }
}