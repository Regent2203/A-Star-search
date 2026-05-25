using ThisProject.Fields.ClickHandlers;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using ThisProject.Views;
using UnityEngine;
using Zenject;

namespace ThisProject.Fields.Implementations
{
    public class GridSceneField<T, V> : SceneField<T, V, Vector2Int>
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
        protected GridClickHandler<T, V> _clickHandler;

        public override IObjectsStorage<T, Vector2Int> Nodes => _nodes;
        public override IObjectsStorage<V, Vector2Int> Views => _views;
        public override IClickHandler ClickHandler => _clickHandler;
        
        public Grid Grid => _grid;
        public Vector2Int CellsNumber => _cellsNumber;


        [Inject]
        public void Construct(GridTypeStorage<T> nodes, GridTypeStorage<V> views, GridClickHandler<T, V> clickHandler, V cellViewPrefab)
        {
            _nodes = nodes;
            _views = views;
            _clickHandler = clickHandler;
            _viewPrefab = cellViewPrefab;
        }

        protected void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            _clickHandler.SetConfiguration(NotifyNodeClicked, NotifyFieldClicked);

            _collider.size = (Vector2)_grid.cellSize * _cellsNumber;
            _collider.offset = _collider.size * 0.5f;

            if (_doCentering)
            {
                transform.position -= 0.5f * Vector3.Scale(_grid.cellSize, new Vector3(_cellsNumber.x, _cellsNumber.y, 0));
            }

            _scaleFactor = _grid.cellSize / _viewPrefab.GetSize();
        }

        public void SetFieldData(T[,] nodes, V[,] views)
        {
            _nodes.SetData(nodes);
            _views.SetData(views);
        }

        protected void OnNodePositionChanged(Vector2 pos)
        {
            //todo
        }
    }
}