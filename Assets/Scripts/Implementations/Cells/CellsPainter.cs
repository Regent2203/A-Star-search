using Core.PathFinders;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Cells
{
    public class CellsPainter
    {
        private CellType _lmbType;
        private CellType _rmbType;
        private KeyCode _markingKeyCode = KeyCode.LeftShift;

        public event Action<CellType> LMBTypeSet;
        public event Action<CellType> RMBTypeSet;

        private IPathFinder<Cell> _pathFinder;


        public CellsPainter(IPathFinder<Cell> pathFinder, [Inject(Id = "MarkingKey")] KeyCode markingKeyCode)
        {
            _pathFinder = pathFinder;
            _markingKeyCode = markingKeyCode;
        }

        public void SetLMBType(CellType cellType)
        {
            _lmbType = cellType;
            LMBTypeSet?.Invoke(cellType);
        }

        public void SetRMBType(CellType cellType)
        {
            _rmbType = cellType;
            RMBTypeSet?.Invoke(cellType);
        }

        public void TryChangeCell(Cell cell, PointerEventData.InputButton btn)
        {
            bool isMarkingMode = Input.GetKey(_markingKeyCode);

            if (isMarkingMode)
                TryMarkCell(cell, btn);
            else
                TrySetCellType(cell, btn);
        }

        private void TrySetCellType(Cell cell, PointerEventData.InputButton btn)
        {
            CellType cellType = null;

            if (btn == PointerEventData.InputButton.Left) //lmb
            {
                cellType = _lmbType;
            }
            else if (btn == PointerEventData.InputButton.Right) //rmb
            {
                cellType = _rmbType;
            }

            if (cellType != null)
                cell.ChangeType(cellType);
        }

        private void TryMarkCell(Cell cell, PointerEventData.InputButton btn)
        {
            if (Input.GetKey(KeyCode.LeftShift))
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