using Fields;
using System;
using UnityEngine;

namespace Nodes.Cells.CellStates
{
    public class CellStateStart : CellState
    {
        public CellStateStart(CellView cellView, AbstractField field, SpriteRenderer spriteRenderer, CellSprites cellSprites) : base(cellView, field, spriteRenderer, cellSprites)
        {
        }

        public override void OnMouseOver()
        {
            if (_field.Mode == FieldMode.SelectObstacles)
            {
                if (Input.GetMouseButton(1))
                {
                    _cellView.ChangeState(new CellStateNormal(_cellView, _field, _spriteRenderer, _cellSprites));
                }
            }
        }

        protected override void Init()
        {
            _spriteRenderer.sprite = _cellSprites.Start;
            
            _field.SetStartNode(_cellView);             
        }
    }
}
