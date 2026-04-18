using UnityEngine;
using System.Collections.Generic;
using Links;
using Nodes.Cells.CellStates;
using Zenject;
using System;
using UnityEngine.EventSystems;

namespace Nodes.Cells
{
    public class Cell : MonoBehaviour, INode
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private CellState _cellState;
        private Vector2Int _index;
        private List<ILink> _links = new List<ILink>();

        public CellState CellState => _cellState;
        public List<ILink> Links => _links;

        public event Action<Cell> CellClicked;
        public event Action<Cell, CellState> CellStateChanged;

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

            ResetState();
        }

        public bool IsObstacle => CellState is CellStateBlocked;

        public Vector3 GetCenterCoords()
        {
            return _spriteRenderer.bounds.center;
        }

        public void DrawPath(bool show)
        {
            if (show)
                ChangeState(_instantiator.Instantiate<CellStateWay>());
            else
                ChangeState(_instantiator.Instantiate<CellStateNormal>());
        }

        public void ResetState()
        {
            ChangeState(_instantiator.Instantiate<CellStateNormal>());
        }

        public Vector2 GetSize()
        {
            return _spriteRenderer.size * (Vector2)transform.localScale;
        }

        public void ChangeState(CellState state)
        {
            _cellState = state;
            _spriteRenderer.sprite = state.Sprite;
            
            CellStateChanged?.Invoke(this, state);
        }


        private void OnMouseOver()
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
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