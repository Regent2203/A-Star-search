using UnityEngine;

namespace Core.Implementations.Cells
{
    public abstract class CellType
    {
        protected Sprite _sprite;
        protected float _weight;

        public Sprite Sprite => _sprite;
        public float Weight => _weight;
    }
}
