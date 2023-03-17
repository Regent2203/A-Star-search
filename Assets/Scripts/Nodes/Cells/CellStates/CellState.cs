using Fields;
using UnityEngine;

namespace Nodes.Cells.CellStates
{
    public abstract class CellState
    {
        protected CellView _cellView; //parent
        protected AbstractField _field;
        protected SpriteRenderer _spriteRenderer;
        protected CellSprites _cellSprites;


        public CellState(CellView cellView, AbstractField field, SpriteRenderer spriteRenderer, CellSprites cellSprites)
        {
            _field = field;
            _cellView = cellView;
            _spriteRenderer = spriteRenderer;
            _cellSprites = cellSprites;

            Init();
        }

        public abstract void OnMouseOver();

        protected abstract void Init();
    }
}
