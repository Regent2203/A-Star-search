using Nodes.Cells;
using Nodes.Cells.CellStates;
using UnityEngine;
using Zenject;

namespace Fields
{
    public class CellGridFieldEditor
    {
        private IInstantiator _instantiator;

        public CellGridFieldEditor(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void ChangeCell(Cell cell)
        {
            CellState state = null;

            if (!Input.GetKey(KeyCode.LeftAlt))
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

            if (state != null && cell.CellState.GetType() != state.GetType())
                cell.ChangeState(state);
        }
    }
}