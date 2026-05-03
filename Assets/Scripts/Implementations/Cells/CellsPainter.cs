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



        public CellsPainter([Inject(Id = "MarkingKey")] KeyCode markingKeyCode)
        {
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

        public void TryChangeCellType(Cell cell, PointerEventData.InputButton btn)
        {
            bool isMarkingMode = Input.GetKey(_markingKeyCode);

            if (!isMarkingMode)
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
        }
    }
}