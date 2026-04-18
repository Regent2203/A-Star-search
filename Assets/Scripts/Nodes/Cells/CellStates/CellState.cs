using UnityEngine;

namespace Nodes.Cells.CellStates
{
    public abstract class CellState
    {
        protected Sprite _sprite;
        protected float _weight;

        public Sprite Sprite => _sprite;
        public float Weight => _weight;
    }
}
