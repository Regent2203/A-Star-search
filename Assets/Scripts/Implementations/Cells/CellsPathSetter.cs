using Core.PathFinders;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsPathSetter
    {
        private readonly IPathFinder<CellNode, Vector2Int> _pathFinder;
        private readonly KeyCode _markingKeyCode = KeyCode.LeftShift;
        

        public CellsPathSetter(IPathFinder<CellNode, Vector2Int> pathFinder, [Inject(Id = "MarkingKey")] KeyCode markingKeyCode)
        {
            _pathFinder = pathFinder;
            _markingKeyCode = markingKeyCode;
        }

        public void TryUseCell(CellNode node, PointerEventData.InputButton btn)
        {
            bool isMarkingMode = Input.GetKey(_markingKeyCode);

            if (isMarkingMode)
            {
                if (btn == PointerEventData.InputButton.Left) //lmb
                {
                    _pathFinder.UpdateStartNode(node);
                }
                else if (btn == PointerEventData.InputButton.Right) //rmb
                {
                    _pathFinder.UpdateFinishNode(node);
                }
            }
        }
    }
}