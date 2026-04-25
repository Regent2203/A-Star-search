using Core.Installers;

namespace Core.Implementations.Cells
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
