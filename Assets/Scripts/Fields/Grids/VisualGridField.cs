using Core.Inputs;
using Core.Nodes;
using Core.ObjectsStorages;
using Core.Views;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Fields.Grids
{
    public class VisualGridField<T, V> : VisualField<T, V, Vector2Int>
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

        protected Vector2 _scaleFactor;

        protected GridFieldCore<T> _core;
        protected GridFieldClickHandler<T, V> _clickHandler;
        protected GridTypeStorage<V> _views;

        protected override IField<T, Vector2Int> Core => _core;
        protected override IClickHandler ClickHandler => _clickHandler;
        public override IObjectsStorage<V, Vector2Int> Views => _views;

        public Grid Grid => _grid;
        public Vector2Int CellsNumber => _cellsNumber;


        [Inject]
        public void Construct(GridTypeStorage<V> views, GridFieldCore<T> core, GridFieldClickHandler<T, V> clickHandler, V cellViewPrefab)
        {
            _views = views;
            _core = core;
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
            _core.SetNodesData(nodes);
            _views.SetData(views);
        }

        protected void OnNodePositionChanged(Vector2 pos)
        {
            //todo
        }
    }
}