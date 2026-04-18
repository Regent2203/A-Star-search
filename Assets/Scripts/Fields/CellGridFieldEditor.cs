using Core.Installers;
using Nodes.Cells;
using Nodes.Cells.CellStates;
using UnityEngine;
using Zenject;

namespace Fields
{
    public class CellGridFieldEditor
    {
        private IInstantiator _instantiator;
        private AbstractField _field;

        public CellGridFieldEditor(IInstantiator instantiator, AbstractField field)
        {
            _instantiator = instantiator;
            _field = field;
        }

        public void RedrawCell(Cell cell)
        {
            CellState state = null;

            if (!Input.GetKeyDown(KeyCode.LeftAlt))
            {
                if (Input.GetMouseButton(0)) //lmb
                {
                    state = _instantiator.Instantiate<CellStateBlocked>();
                }
                else if (Input.GetMouseButton(1)) //rmb
                {
                    state = _instantiator.Instantiate<CellStateNormal>();
                }
            }
            else
            {
                if (Input.GetMouseButton(0)) //lmb
                {
                    state = _instantiator.Instantiate<CellStateStart>();
                }
                else if (Input.GetMouseButton(1)) //rmb
                {
                    state = _instantiator.Instantiate<CellStateFinish>();
                }
            }

            cell.ChangeState(state);
        }
    }
}