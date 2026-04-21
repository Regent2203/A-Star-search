using Core.Nodes.Cells;
using Core.PathFinders;
using UnityEngine;

namespace Core.Nodes.Cells
{
    public class CellsMarker
    {
        private IPathFinder<Cell> _pathFinder;


        public CellsMarker(IPathFinder<Cell> pathFinder)
        {
            _pathFinder = pathFinder;
        }

        public void MarkCell(Cell cell)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (Input.GetMouseButton(0)) //lmb
                {
                    _pathFinder.SetStartNode(cell);
                    cell.ShowStartMarker(true);
                }
                else if (Input.GetMouseButton(1)) //rmb
                {
                    _pathFinder.SetFinishNode(cell);
                    cell.ShowFinishMarker(true);
                }
            }
        }
    }
}
