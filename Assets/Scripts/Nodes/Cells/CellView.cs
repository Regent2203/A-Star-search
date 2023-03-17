using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Fields;
using Links;
using Nodes.Cells.CellStates;

namespace Nodes.Cells
{
    public class CellView : MonoBehaviour, INode, IView
    {
        [SerializeField]
        private CellSprites _cellSprites = default;
        [SerializeField]
        private SpriteRenderer _spriteRenderer = default;
        [SerializeField]
        private BoxCollider2D _boxCollider2D = default;

        private CellState _cellState;
        private Vector2Int _index;
        private List<ILink> _links = new List<ILink>();
        private AbstractField _field;

        public CellState CellState => _cellState;
        public Vector2Int Index => _index;
        public List<ILink> Links => _links;


        public void Init(AbstractField field, Vector2Int index)
        {
            _field = field;
            _index = index;

            ResetState();
        }

        public bool IsObstacle => CellState is CellStateBlocked;

        public Vector3 GetCenter()
        {
            return _boxCollider2D.bounds.center;
        }

        public void DrawPath()
        {
            ChangeState(new CellStateWay(this, _field, _spriteRenderer, _cellSprites));
        }

        public void ResetState()
        {
            ChangeState(new CellStateNormal(this, _field, _spriteRenderer, _cellSprites));
        }

        public Vector2 GetSize()
        {
            return _boxCollider2D.size * (Vector2)transform.localScale;
        }

        public void SetScale(Vector2 scale)
        {
            transform.localScale = scale;
        }        

        private void OnMouseOver()
        {
            _cellState.OnMouseOver();            
        }

        public void ChangeState(CellState newState)
        {
            _cellState = newState;
        }

        private void OnDrawGizmos()
        {
            foreach (var l in _links)
                Gizmos.DrawLine(l.From.GetCenter(), l.To.GetCenter());
        }

    }
}