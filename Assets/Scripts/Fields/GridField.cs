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
        [SerializeField]
        protected Vector2Int _cellsNumber = new Vector2Int(10, 10);
        [SerializeField]
        protected bool _doCentering = true;

        protected V _viewPrefab;
        protected Vector2 _scaleFactor;

        protected GridTypeStorage<T> _nodes;
        protected GridTypeStorage<V> _views;

        public override IObjectsStorage<T, Vector2Int> Nodes => _nodes;
        public override IObjectsStorage<V, Vector2Int> Views => _views;

        public override BoxCollider2D Box => _collider;
        public Grid Grid => _grid;
        public Vector2Int CellsNumber => _cellsNumber;


        [Inject]
        public void Construct(GridTypeStorage<T> nodes, GridTypeStorage<V> views, V cellViewPrefab)
        {
            _nodes = nodes;
            _views = views;
            _viewPrefab = cellViewPrefab;
        }

        protected void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            PrepareGrid();
        }

        public void SetFieldData(T[,] nodes, V[,] views)
        {
            _nodes.ClearData();
            _views.ClearData();

            _nodes.SetData(nodes);
            _views.SetData(views);
        }

        private void PrepareGrid()
        {
            _collider.size = (Vector2)_grid.cellSize * _cellsNumber;
            _collider.offset = _collider.size * 0.5f;

            if (_doCentering)
            {
                transform.position -= 0.5f * Vector3.Scale(_grid.cellSize, new Vector3(_cellsNumber.x, _cellsNumber.y, 0));
            }

            _scaleFactor = _grid.cellSize / _viewPrefab.GetSize();
        }

        public Vector2Int PositionToIndex(Vector2 coords)
        {
            var localPos = transform.InverseTransformPoint(coords);

            int x = Mathf.FloorToInt(localPos.x / _grid.cellSize.x);
            int y = Mathf.FloorToInt(localPos.y / _grid.cellSize.y);

            return new Vector2Int(x, y);
        }
    }
}