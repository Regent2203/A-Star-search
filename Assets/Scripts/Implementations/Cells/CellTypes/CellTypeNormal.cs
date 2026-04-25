using Core.Installers;

namespace Core.Implementations.Cells
{
    public class CellTypeNormal : CellType
    {
        public CellTypeNormal(CellSprites cellSprites)
        {
            _sprite = cellSprites.Normal;
            _weight = 1f;
        }
    }
}
