using System;
using UnityEngine.EventSystems;

namespace Core.Implementations.Cells
{
    public class CellsPainter
    {
        private CellType _lmbType;
        private CellType _rmbType;

        public event Action<CellType> LMBBrushSet;
        public event Action<CellType> RMBBrushSet;

        public void SetLMBType(CellType cellType)
        {
            _lmbType = cellType;
            LMBBrushSet?.Invoke(cellType);
        }

        public void SetRMBType(CellType cellType)
        {
            _rmbType = cellType;
            RMBBrushSet?.Invoke(cellType);
        }

        public void TryChangeCellType(CellNode node, PointerEventData.InputButton btn)
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
                node.ChangeType(cellType);
        }
    }
}