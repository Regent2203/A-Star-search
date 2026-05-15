using Core.Inputs;
using Core.Nodes;
using Core.Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Fields.Grids
{
    public class GridClickHandler<T> where T : class, INode<Vector2Int>
    {
        private GridFieldBase<T> _field;
        private Grid _grid;

        private readonly SignalBus _signalBus;
        private readonly IInputService _inputService;


        public GridClickHandler(SignalBus signalBus, IInputService inputService)
        {
            _signalBus = signalBus;
            _inputService = inputService;
        }

        public void SetConfiguration(GridFieldBase<T> field, Grid grid)
        {
            _field = field;
            _grid = grid;
        }

        public void ProcessClick(PointerEventData eventData)
        {
            Vector3 localPos = _field.transform.InverseTransformPoint(eventData.pointerCurrentRaycast.worldPosition);

            int x = Mathf.FloorToInt(localPos.x / _grid.cellSize.x);
            int y = Mathf.FloorToInt(localPos.y / _grid.cellSize.y);

            var node = _field.GetNodeById(new Vector2Int(x, y));
            if (node != null)
            {
                _signalBus.Fire(new NodeClickedSignal<T>(node, eventData.button, _inputService.CreateSnapshot()));
            }
        }
    }
}