using Core.Installers;

namespace Nodes.Cells.CellStates
{
    public class CellStateStart : CellState
    {
        public CellStateStart(CellSprites cellSprites)
        {
            _sprite = cellSprites.Start;
            _weight = 0f;
        }
    }
}
