using Core.Nodes;
using UnityEngine;

namespace Core.Fields.Grids
{
    public class GridFieldBase<T> : MonoBehaviour, IGraph<T, Vector2Int> 
        where T : class, INode<Vector2Int>
    {
        [SerializeField]
        protected Grid _grid;
        [SerializeField]
        protected BoxCollider2D _collider;
        [SerializeField]
        protected Vector2Int _cellsNumber = new Vector2Int(10, 10);
        [SerializeField]
        protected bool _doCentering = true;

        protected T[,] _nodes;
        
        public Vector2Int CellsNumber => _cellsNumber;

        
        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            _collider.size = (Vector2)_grid.cellSize * _cellsNumber;
            _collider.offset = _collider.size * 0.5f;

            if (_doCentering)
                DoCentering();
        }

        private void DoCentering()
        {
            transform.position -= 0.5f * Vector3.Scale(_grid.cellSize, new Vector3(_cellsNumber.x, _cellsNumber.y, 0));
        }

        public void SetData(T[,] nodes)
        {
            _nodes = nodes;
        }

        public T GetNodeById(Vector2Int id)
        {
            if (_nodes.IsWithinBounds(id.x, id.y))
                return _nodes[id.x, id.y];

            return null;
        }
    }
}