using Core.PathFinders;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsMarker
    {
        private readonly KeyCode _markingKeyCode = KeyCode.LeftShift;
        private readonly IPathFinder<Cell> _pathFinder;


        public CellsMarker(IPathFinder<Cell> pathFinder, [Inject(Id = "MarkingKey")] KeyCode markingKeyCode)
        {
            _pathFinder = pathFinder;
            _markingKeyCode = markingKeyCode;
        }

        public void TryMarkCell(Cell cell, PointerEventData.InputButton btn)
        {
            bool isMarkingMode = Input.GetKey(_markingKeyCode);

            if (isMarkingMode)
            {
                if (btn == PointerEventData.InputButton.Left) //lmb
                {
                    _pathFinder.UpdateStartNode(cell);
                }
                else if (btn == PointerEventData.InputButton.Right) //rmb
                {
                    _pathFinder.UpdateFinishNode(cell);
                }
            }
        }
    }
}