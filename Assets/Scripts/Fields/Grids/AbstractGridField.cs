using Core.Links;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Fields.Grids
{
    public abstract class AbstractGridField<T> : MonoBehaviour, IInitializable, IField<T> where T : class, INode<T>
    {
        [SerializeField]
        protected Grid _grid;
        [SerializeField]
        protected BoxCollider2D _collider;
        [SerializeField]
        protected Vector2Int _cellsNumber = new Vector2Int(10, 10);
        [SerializeField]
        protected bool _doCentering = true;

        protected IView _viewPrefab;
        protected Vector2 _scaleFactor;
        protected T[,] _nodes;

        
        public void Initialize() //zenject
        {
            InitGrid();
            Init();
        }
        
        private void InitGrid()
        {
            _nodes = new T[_cellsNumber.x, _cellsNumber.y];

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

        protected virtual void Init() { }

        public abstract IEnumerable<ILink<T>> GetLinksForNode(T node);

        public T GetNodeByIndex(int i, int j)
        {
            if (_nodes.IsWithinBounds(i, j))
                return _nodes[i, j];

            return null;
        }
    }
}