using Core.Inputs;
using Core.Nodes;
using Core.ObjectsStorages;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Fields.Grids
{
    public class GridFieldBase<T> : MonoBehaviour, IPointerDownHandler
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

        protected GridTypeStorage<T> _nodes;
        protected GridFieldClickHandler<T> _clickHandler;
        
        public Grid Grid => _grid;
        public Vector2Int CellsNumber => _cellsNumber;
        public GridTypeStorage<T> Nodes => _nodes;
        public event Action<T, PointerEventData.InputButton, InputSnapshot> NodeClicked;


        [Inject]
        public void Construct(GridTypeStorage<T> nodes, GridFieldClickHandler<T> clickHandler)
        {
            _nodes = nodes;
            _clickHandler = clickHandler;
        }

        private void Awake()
        {
            Init();
        }

        protected virtual void Init()
        {
            _clickHandler.SetConfiguration(NotifyNodeClicked);

            _collider.size = (Vector2)_grid.cellSize * _cellsNumber;
            _collider.offset = _collider.size * 0.5f;

            if (_doCentering)
                DoCentering();
        }

        private void DoCentering()
        {
            transform.position -= 0.5f * Vector3.Scale(_grid.cellSize, new Vector3(_cellsNumber.x, _cellsNumber.y, 0));
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _clickHandler.ProcessClick(eventData);
        }

        private void NotifyNodeClicked(T node, PointerEventData.InputButton btn, InputSnapshot input)
        {
            NodeClicked?.Invoke(node, btn, input);
        }
    }
}