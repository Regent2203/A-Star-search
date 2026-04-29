using Core.Installers;

namespace Core.Implementations.Cells
{
    public class CellTypeDirt : CellType
    {
        public CellTypeDirt(CellSprites cellSprites)
        {
            _sprite = cellSprites.Dirt;
            _weight = 1.25f;
        }
    }
}
