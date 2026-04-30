using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Fields
{
    public abstract class AbstractGridField<T> : MonoBehaviour, IInitializable, IField<T> where T : INode<T>
    {
        [SerializeField]
        protected Grid _grid;
        [SerializeField]
        protected Vector2Int _cellsNumber = new Vector2Int(10, 10);
        [SerializeField]
        protected bool _doCentering = true;

        protected abstract IView _nodePrefab { get; }
        protected Vector2 _scaleFactor;
        protected T[,] _gridNodes;

        public abstract IReadOnlyList<T> Nodes { get; }


        public void Initialize() //zenject
        {
            InitGrid();
            Init();
        }
        
        private void InitGrid()
        {
            _gridNodes = new T[_cellsNumber.x, _cellsNumber.y];

            _scaleFactor = _grid.cellSize / _nodePrefab.GetSize();

            if (_doCentering)
                DoCentering();
        }

        private void DoCentering()
        {
            transform.position -= 0.5f * Vector3.Scale(_grid.cellSize, new Vector3(_cellsNumber.x, _cellsNumber.y, 0));
        }

        protected virtual void Init() { }
    }
}