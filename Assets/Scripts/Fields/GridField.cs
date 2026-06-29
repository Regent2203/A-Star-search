using ThisProject.Nodes;
using UnityEngine;
using Zenject;

namespace ThisProject.Fields
{
    public class GridField : Field
    {
        [SerializeField]
        protected Grid _grid;

        protected Vector2Int _size;
        protected Vector2 _scaleFactor;

        public override Vector2 ScaleFactor => _scaleFactor;
        public Grid Grid => _grid;

        

        [Inject]
        public void Construct(INodeView viewPrefab)
        {
            CalculateScaleFactor(viewPrefab);
        }

        private void CalculateScaleFactor(INodeView viewPrefab)
        {
            _scaleFactor = _grid.cellSize / viewPrefab.GetSize();
        }

        public void SetSize(Vector2Int size)
        {
            _size = size;
            UpdateColliderSize();
        }

        private void UpdateColliderSize()
        {
            _collider.size = _grid.cellSize * new Vector2(_size.x, _size.y);
        }

        public Vector2Int PositionToIndex(Vector2 coords)
        {
            var localPos = transform.InverseTransformPoint(coords);

            int x = Mathf.FloorToInt(localPos.x / _grid.cellSize.x + _size.x / 2f);
            int y = Mathf.FloorToInt(localPos.y / _grid.cellSize.y + _size.y / 2f);

            return new Vector2Int(x, y);
        }
    }
}