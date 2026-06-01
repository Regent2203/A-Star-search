using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;
using Zenject;

namespace ThisProject.Fields
{
    public abstract class GridField<T, V> : VisibleField<T, V, Vector2Int>
        where T : class, INode<Vector2Int>
        where V : class, IView<Vector2Int>
    {
        [SerializeField]
        protected Grid _grid;
        [SerializeField]
        protected BoxCollider2D _collider;

        protected Vector2Int _size;
        protected V _viewPrefab;
        protected Vector2 _scaleFactor;

        protected GridTypeStorage<T> _nodes;
        protected GridTypeStorage<V> _views;

        public override IObjectsStorage<T, Vector2Int> Nodes => _nodes;
        public override IObjectsStorage<V, Vector2Int> Views => _views;

        public override BoxCollider2D Box => _collider;
        public Grid Grid => _grid;


        [Inject]
        public void Construct(GridTypeStorage<T> nodes, GridTypeStorage<V> views, V cellViewPrefab)
        {
            _nodes = nodes;
            _views = views;
            _viewPrefab = cellViewPrefab;
        }

        protected virtual void Awake()
        {
            CalculateScaleFactor();
        }

        private void CalculateScaleFactor()
        {
            _scaleFactor = _grid.cellSize / _viewPrefab.GetSize();
        }

        public void SetFieldData(T[,] nodes, V[,] views, Vector2Int size)
        {
            _nodes.ClearData();
            _views.ClearData();

            _nodes.SetData(nodes);
            _views.SetData(views);

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