using Nodes;
using UnityEngine;

namespace Fields
{
    public abstract class AbstractGridField<T> : AbstractField where T : INode
    {
        [SerializeField]
        protected Grid _grid;
        [SerializeField]
        protected Vector2Int _cellsNumber = new Vector2Int(10, 10);
        [SerializeField]
        protected bool _doCentering = true;

        protected abstract IView _nodePrefab { get; }
        protected Vector2 _scaleFactor;
        protected Vector3 _cellSize;

        protected INode[,] _gridNodes;


        protected virtual void Awake()
        {
            _gridNodes = new INode[_cellsNumber.x, _cellsNumber.y];

            _cellSize = _grid.cellSize;
            _scaleFactor = _cellSize / _nodePrefab.GetSize();

            if (_doCentering)
                transform.position -= 0.5f * Vector3.Scale(_cellSize, new Vector3(_cellsNumber.x, _cellsNumber.y, 0));

            //clears path when we start changing setup
            /*
            ModeChangedPrevious += (prevMode) =>
            {
                if (prevMode == DrawMode.Launch)
                    ShowPath(false, _path, true);
            };*/
        }
    }
}