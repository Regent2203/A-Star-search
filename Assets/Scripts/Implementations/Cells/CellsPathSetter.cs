using Core.PathFinders;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsPathSetter
    {
        private readonly KeyCode _markingKeyCode = KeyCode.LeftShift;
        private readonly IPathFinder _pathFinder;


        public CellsPathSetter(IPathFinder pathFinder, [Inject(Id = "MarkingKey")] KeyCode markingKeyCode)
        {
            _pathFinder = pathFinder;
            _markingKeyCode = markingKeyCode;
        }

        public void TryUseCell(CellView cell, PointerEventData.InputButton btn)
        {
            bool isMarkingMode = Input.GetKey(_markingKeyCode);

            if (isMarkingMode)
            {
                if (btn == PointerEventData.InputButton.Left) //lmb
                {
                    _pathFinder.UpdateStartNode(cell.Node);
                }
                else if (btn == PointerEventData.InputButton.Right) //rmb
                {
                    _pathFinder.UpdateFinishNode(cell.Node);
                }
            }
        }
    }
}