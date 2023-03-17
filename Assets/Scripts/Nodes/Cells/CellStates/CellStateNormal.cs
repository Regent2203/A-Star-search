using Fields;
using System;
using UnityEngine;

namespace Nodes.Cells.CellStates
{
    public class CellStateNormal : CellState
    {
        public CellStateNormal(CellView cellView, AbstractField field, SpriteRenderer spriteRenderer, CellSprites cellSprites) : base(cellView, field, spriteRenderer, cellSprites)
        {
        }

        public override void OnMouseOver()
        {
            if (_field.Mode == FieldMode.SelectObstacles)
            {
                if (Input.GetMouseButton(0))
                {
                    _cellView.ChangeState(new CellStateBlocked(_cellView, _field, _spriteRenderer, _cellSprites));
                }
            }
            else
            if (_field.Mode == FieldMode.SelectStartFinish)
            {
                if (Input.GetMouseButton(0))
                {
                    _cellView.ChangeState(new CellStateStart(_cellView, _field, _spriteRenderer, _cellSprites));
                }
                else
                if (Input.GetMouseButton(1))
                {
                    _cellView.ChangeState(new CellStateFinish(_cellView, _field, _spriteRenderer, _cellSprites));
                }
            }
        }

        protected override void Init()
        {
            _spriteRenderer.sprite = _cellSprites.Normal;
        }
    }
}
