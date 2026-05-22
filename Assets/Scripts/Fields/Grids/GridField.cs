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
    public class GridField<T, V> : MonoBehaviour, IPointerDownHandler, IVisualField<T, V, Vector2Int>
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

        protected GridFieldCore<T> _core;

        protected V _viewPrefab;
        protected Vector2 _scaleFactor;

        protected GridTypeStorage<V> _views;
        public IObjectsStorage<V, Vector2Int> Views => _views;

        protected GridFieldClickHandler<T, V> _clickHandler;

        public Grid Grid => _grid;
        public Vector2Int CellsNumber => _cellsNumber;

        public IObjectsStorage<T, Vector2Int> Nodes => _core.Nodes;

        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;
        public event Action FieldChanged; //todo _core

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
            _clickHandler.SetConfiguration(NotifyNodeClicked);

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

        public V GetViewById(Vector2Int id)
        {
            return _views.GetById(id);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_clickHandler.ProcessClick(eventData))
            {
                //FieldClicked?.Invoke(this, eventData.button, _inputService.CreateSnapshot()); //todo
            }
        }

        protected void NotifyNodeClicked(T node, PointerEventData.InputButton btn, InputSnapshot input)
        {
            NodeClicked?.Invoke(node, btn, input);
        }

        public T GetNodeById(Vector2Int id)
        {
            return _core.GetNodeById(id);
        }
    }
}