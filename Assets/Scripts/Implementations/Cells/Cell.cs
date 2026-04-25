using Core.Links;
using Core.Nodes;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Core.Implementations.Cells
{
    public class Cell : MonoBehaviour, INode<Cell>
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
        private List<ILink<Cell>> _links = new();

        public CellType CellType => _cellType;
        public List<ILink<Cell>> Links => _links;

        public event Action<Cell> CellClicked;
        public event Action<Cell, CellType> CellTypeChanged;

        private IInstantiator _instantiator;


        [Inject]
        public void Construct(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public void Init(Vector2Int index, Vector2 scale)
        {
            _index = index;
            transform.localScale = scale;
            name = $"Cell {index.x},{index.y}";

            ChangeType(_instantiator.Instantiate<CellTypeNormal>());

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


        public void ChangeType(CellType cellType)
        {
            if (_cellType == cellType)
                return;

            _cellType = cellType;
            _spriteRenderer.sprite = cellType.Sprite;
            
            CellTypeChanged?.Invoke(this, cellType);
        }

        private void OnMouseOver()
        {
            if (Input.anyKey)
                Debug.Log(_index);

            if (Input.anyKey)
                CellClicked?.Invoke(this);
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