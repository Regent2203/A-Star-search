using Core.Links;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Fields.Grids
{
    public abstract class AbstractGridField : MonoBehaviour, IInitializable, IField
    {
        [SerializeField]
        protected Grid _grid;
        [SerializeField]
        protected Vector2Int _cellsNumber = new Vector2Int(10, 10);
        [SerializeField]
        protected bool _doCentering = true;

        protected IView _viewPrefab;
        protected Vector2 _scaleFactor;
        protected INode[,] _nodes;


        public void Initialize() //zenject
        {
            InitGrid();
            Init();
        }
        
        private void InitGrid()
        {
            _nodes = new INode[_cellsNumber.x, _cellsNumber.y];

            _scaleFactor = _grid.cellSize / _viewPrefab.GetSize();

            if (_doCentering)
                DoCentering();
        }

        private void DoCentering()
        {
            transform.position -= 0.5f * Vector3.Scale(_grid.cellSize, new Vector3(_cellsNumber.x, _cellsNumber.y, 0));
        }

        protected virtual void Init() { }


        public List<ILink> GetNeighboursFor(Vector2Int index)
        {
            return new List<ILink>(); //todo
        }
    }
}