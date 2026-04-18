using Fields;
using System;
using UnityEngine;
using static Core.Installers.SpritesLibraryInstaller;

namespace Nodes.Cells.CellStates
{
    public class CellStateWay : CellState
    {
        public CellStateWay(CellView cellView, AbstractField field, SpriteRenderer spriteRenderer, CellSprites cellSprites) : base(cellView, field, spriteRenderer, cellSprites)
        {
        }

        public override void OnMouseOver()
        {
            if (_field.Mode == DrawMode.SelectObstacles)
            {
                if (Input.GetMouseButton(1))
                {
                    _cellView.ChangeState(new CellStateNormal(_cellView, _field, _spriteRenderer, _cellSprites));
                }
            }
        }

        protected override void Init()
        {
            _spriteRenderer.sprite = _cellSprites.Way;
        }
    }
}
