using Core.Nodes;
using Core.Views;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Core.Fields.Grids
{
    public abstract class AbstractGridField<T, V> : MonoBehaviour, IInitializable, IGraph<T, Vector2Int> 
        where T : class, INode<Vector2Int> 
        where V : class, IView
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

        protected T[,] _nodes;
        protected V[,] _views;
        
        public Vector2Int CellsNumber => _cellsNumber;


        [Inject]
        public void Construct(V cellViewPrefab)
        {
            _viewPrefab = cellViewPrefab;
        }
        
        public void Initialize() //zenject
        {
            SetupGridPhysics();
            Init();
        }
        
        private void SetupGridPhysics()
        {
            _collider.size = (Vector2)_grid.cellSize * _cellsNumber;
            _collider.offset = _collider.size * 0.5f;

            _scaleFactor = _grid.cellSize / _viewPrefab.GetSize();

            if (_doCentering)
                DoCentering();
        }

        private void DoCentering()
        {
            transform.position -= 0.5f * Vector3.Scale(_grid.cellSize, new Vector3(_cellsNumber.x, _cellsNumber.y, 0));
        }

        protected abstract void Init();

        public void SetData(T[,] nodes, V[,] views)
        {
            _nodes = nodes;
            _views = views;
        }

        public T GetNodeById(Vector2Int id)
        {
            if (_nodes.IsWithinBounds(id.x, id.y))
                return _nodes[id.x, id.y];

            return null;
        }

        public V GetViewById(Vector2Int id)
        {
            if (_views.IsWithinBounds(id.x, id.y))
                return _views[id.x, id.y];

            return null;
        }

        public V GetViewForNode(T node) => GetViewById(node.Id);

        public IReadOnlyList<V> GetViewsForNodes(IList<T> nodePath) => nodePath.Select(GetViewForNode).ToList();
    }
}