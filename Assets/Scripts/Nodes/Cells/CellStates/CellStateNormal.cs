using Fields;
using System;
using UnityEngine;
using Core.Installers;

namespace Nodes.Cells.CellStates
{
    public class CellStateNormal : CellState
    {
        public CellStateNormal(CellSprites cellSprites)
        {
            _sprite = cellSprites.Normal;
            _weight = 0f;
        }
    }
}
