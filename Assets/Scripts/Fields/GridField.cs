using EasyField.Nodes;
using UnityEngine;
using Zenject;

namespace EasyField.Fields
{
    public abstract class GridField : Field
    {
        [SerializeField]
        protected Grid _grid;

        protected Vector2Int _size = Vector2Int.zero;
        protected Vector2 _scaleFactor;

        public override Vector2 ScaleFactor => _scaleFactor;
        public Grid Grid => _grid;
        public Vector2Int Size => _size;

        

        [Inject]
        public void Construct(INodeView viewPrefab)
        {
            CalculateScaleFactor(viewPrefab);
        }

        private void CalculateScaleFactor(INodeView viewPrefab)
        {
            _scaleFactor = _grid.cellSize / viewPrefab.GetSize();
        }

        public abstract void SetSize(Vector2Int size);

        public abstract Vector2Int PositionToIndex(Vector2 coords);
    }
}