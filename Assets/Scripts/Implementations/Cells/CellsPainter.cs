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

        private CellsGridField _field;
        private readonly KeyCode _markingKeyCode = KeyCode.LeftShift;

        public event Action<CellType> LMBTypeSet;
        public event Action<CellType> RMBTypeSet;


        public CellsPainter(CellsGridField field, [Inject(Id = "MarkingKey")] KeyCode markingKeyCode)
        {
            _field = field;
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

        public void TryChangeCellType(CellView view, PointerEventData.InputButton btn)
        {
            bool isMarkingMode = Input.GetKey(_markingKeyCode);

            if (!isMarkingMode)
            {
                var index = view.Index;
                var node = _field.GetNodeByIndex(index.x, index.y);

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
                    node.ChangeType(cellType);
            }
        }
    }
}