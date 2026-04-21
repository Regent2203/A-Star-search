using UnityEngine;

namespace Core.Nodes.Cells
{
    public abstract class CellType
    {
        protected Sprite _sprite;
        protected float _weight;

        public Sprite Sprite => _sprite;
        public float Weight => _weight;
    }
}
