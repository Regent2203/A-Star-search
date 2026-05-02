using Core.Links;
using Core.Nodes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Implementations.Cells
{
    public class Cell : MonoBehaviour, INode<Cell>, IPointerDownHandler
    {
        [SerializeField]
        private GameObject _pathMarker;
        [SerializeField]
        private GameObject _startMarker;
        [SerializeField]
        private GameObject _finishMarker;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Vector2Int _index;
        private CellType _cellType;
        private List<ILink<Cell>> _links = new List<ILink<Cell>>();

        public Vector2Int Index => _index;
        public CellType CellType => _cellType;
        public List<ILink<Cell>> Links => _links;

        public bool IsBlocked => float.IsPositiveInfinity(CellType.Weight);

        public event Action<Cell, PointerEventData.InputButton> CellClicked;
        public event Action<Cell, CellType> CellTypeChanged;

        private CellsConfig _cellsConfig;


        [Inject]
        public void Construct(CellsConfig cellsConfig)
        {
            _cellsConfig = cellsConfig;
        }

        public void Init(Vector2Int index, Vector2 scale)
        {
            _index = index;
            transform.localScale = scale;
            name = $"Cell {index.x},{index.y}";

            ChangeType(_cellsConfig.DefaultCellType);

            ShowPathMarker(false);
            ShowStartMarker(false);
            ShowFinishMarker(false);
        }
        

        public void ShowPathMarker(bool show)
        {
            _pathMarker.SetActive(show);
        }
        public void ShowStartMarker(bool show)
        {
            _startMarker.SetActive(show);
        }
        public void ShowFinishMarker(bool show)
        {
            _finishMarker.SetActive(show);
        }


        public Vector2 GetSize()
        {
            return _spriteRenderer.size * (Vector2)transform.localScale;
        }
        public Vector3 GetCenterCoords()
        {
            return _spriteRenderer.bounds.center;
        }

        public Vector2 GetEstimatedPosition()
        {
            var center = _spriteRenderer.bounds.center;
            var scale = transform.localScale;

            return new Vector2(center.x / scale.x, center.y / scale.y);
        }


        public void ChangeType(CellType cellType)
        {
            if (_cellType == cellType)
                return;

            _cellType = cellType;
            _spriteRenderer.sprite = cellType.Sprite;
            
            CellTypeChanged?.Invoke(this, cellType);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            CellClicked?.Invoke(this, eventData.button);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;

            foreach (var l in _links)
            {
                Gizmos.DrawLine(l.From.GetCenterCoords(), l.To.GetCenterCoords());
            }
        }
    }
}