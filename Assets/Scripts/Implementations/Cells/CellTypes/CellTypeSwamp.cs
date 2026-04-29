using Core.Installers;

namespace Core.Implementations.Cells
{
    public class CellTypeSwamp : CellType
    {
        public CellTypeSwamp(CellSprites cellSprites)
        {
            _sprite = cellSprites.Swamp;
            _weight = 1.75f;
        }
    }
}
