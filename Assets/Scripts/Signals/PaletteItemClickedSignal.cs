using Core.Implementations.Cells;
using UnityEngine.EventSystems;

namespace Core.Signals
{
    public readonly struct PaletteItemClickedSignal
    {
        public CellType CellType { get; }
        public PointerEventData.InputButton Button { get; }


        public PaletteItemClickedSignal(CellType cellType, PointerEventData.InputButton btn)
        {
            CellType = cellType;
            Button = btn;
        }
    }
}