using UnityEngine;
using System.Collections.Generic;
using Fields;
using Links;
using Nodes.Cells.CellStates;
using Zenject;
using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

namespace Nodes.Cells
{
    public class Cell : MonoBehaviour, INode, IView
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private CellState _cellState;
        private Vector2Int _index;
        private List<ILink> _links = new List<ILink>();

        public CellState CellState => _cellState;
        public List<ILink> Links => _links;

        public event Action<Cell> CellClicked;
        public event Action<CellState> CellStateChanged;

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

            ChangeState(_instantiator.Instantiate<CellStateNormal>());
        }

        public bool IsObstacle => CellState is CellStateBlocked;

        public Vector3 GetCenterCoords()
        {
            return _spriteRenderer.bounds.center;
        }

        public void DrawPath(bool draw)
        {
            if (draw)
                ChangeState(_instantiator.Instantiate<CellStateWay>());
            else
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

            CellStateChanged?.Invoke(state);
        }

        private void OnMouseDown()
        {
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