using Core.Installers;

namespace Core.Implementations.Cells
{
    public class CellTypeObstacle : CellType
    {
        public CellTypeObstacle(CellSprites cellSprites)
        {
            _sprite = cellSprites.Obstacle;
            _weight = float.PositiveInfinity;
        }
    }
}
