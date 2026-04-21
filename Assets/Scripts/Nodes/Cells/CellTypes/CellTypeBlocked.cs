using Core.Installers;

namespace Core.Nodes.Cells
{
    public class CellTypeBlocked : CellType
    {
        public CellTypeBlocked(CellSprites cellSprites)
        {
            _sprite = cellSprites.Blocked;
            _weight = float.PositiveInfinity;
        }
    }
}
