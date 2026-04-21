using Core.Nodes.Cells;
using UnityEngine;
using Zenject;

namespace Core.Fields
{
    public class CellPainter
    {
        private IInstantiator _instantiator;

        public CellPainter(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void ChangeCellType(Cell cell)
        {
            CellType cellType = null;

            if (!Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetMouseButton(0)) //lmb
                {
                    cellType = _instantiator.Instantiate<CellTypeBlocked>();
                }
                else if (Input.GetMouseButton(1)) //rmb
                {
                    cellType = _instantiator.Instantiate<CellTypeNormal>();
                }
            }
            else
            {
                /*
                if (Input.GetMouseButton(0)) //lmb
                {
                    cellType = _instantiator.Instantiate<CellStateStart>();
                }
                else if (Input.GetMouseButton(1)) //rmb
                {
                    cellType = _instantiator.Instantiate<CellStateFinish>();
                }
                */
            }

            if (cellType != null && cell.CellType.GetType() != cellType.GetType())
                cell.ChangeType(cellType);
        }
    }
}