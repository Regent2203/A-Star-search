using Core.Installers;

namespace Core.Implementations.Cells
{
    public class CellTypeSand : CellType
    {
        public CellTypeSand(CellSprites cellSprites)
        {
            _sprite = cellSprites.Sand;
            _weight = 1.5f;
        }
    }
}
