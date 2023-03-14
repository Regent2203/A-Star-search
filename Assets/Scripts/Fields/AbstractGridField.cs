using Nodes;
using UnityEngine;

namespace Fields
{
    public abstract class AbstractGridField : AbstractField
    {
        [SerializeField]
        protected Grid _grid = default;
        [SerializeField]
        protected Vector2Int _size = new Vector2Int(10, 10);
        [SerializeField]
        protected bool _doCentering = true;

        protected abstract IView _nodePrefab { get; }
        protected Vector2 _scaleMod;
        protected Vector3 _cellSize;

        protected INode[,] _gridNodes;


        protected virtual void Awake()
        {
            //clears path when we start changing setup
            ModeChangedPrevious += (prevMode) =>
            {
                if (prevMode == FieldMode.Launch)
                    ShowPath(false, _path, true);
            };
        }

        public override void Initialize()
        {
            _gridNodes = new INode[_size.x, _size.y];

            _cellSize = _grid.cellSize;
            _scaleMod = _cellSize / _nodePrefab.GetSize();

            if (_doCentering)
                this.transform.position -= 0.5f * Vector3.Scale(_cellSize, new Vector3(_size.x, _size.y, 0));
        }
    }
}