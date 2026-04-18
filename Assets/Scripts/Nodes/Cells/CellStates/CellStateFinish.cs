using Core.Installers;

namespace Nodes.Cells.CellStates
{
    public class CellStateFinish : CellState
    {
        public CellStateFinish(CellSprites cellSprites)
        {
            _sprite = cellSprites.Finish;
            _weight = 0f;
        }
    }
}
