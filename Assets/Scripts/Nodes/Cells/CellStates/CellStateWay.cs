using Core.Installers;

namespace Nodes.Cells.CellStates
{
    public class CellStateWay : CellState
    {
        public CellStateWay(CellSprites cellSprites)
        {
            _sprite = cellSprites.Way;
            _weight = 0f;
        }
    }
}
