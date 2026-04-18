using Core.Installers;

namespace Nodes.Cells.CellStates
{
    public class CellStateBlocked : CellState
    {
        public CellStateBlocked(CellSprites cellSprites)
        {
            _sprite = cellSprites.Blocked;
            _weight = float.PositiveInfinity;
        }
    }
}
