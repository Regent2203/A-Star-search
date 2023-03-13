using UnityEngine;
using UnityEngine.EventSystems;
using Algorithm;
using Demo;
using System.Collections.Generic;
using Links;

namespace Cells
{
    public class CellView : MonoBehaviour, INode
    {
        [SerializeField]
        private CellSprites _cellSprites = default;
        [SerializeField]
        private SpriteRenderer _spriteRenderer = default;
        [SerializeField]
        private BoxCollider2D _boxCollider2D = default;

        private CellState _cellState = CellState.Normal;
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
        }

        public Vector2 GetSize()
        {
            return _boxCollider2D.size * (Vector2)transform.localScale;
        }

        public Vector3 GetCenter()
        {
            return _boxCollider2D.bounds.center;
        }

        public void SetScale(Vector2 scale)
        {
            transform.localScale = scale;
        }        

        private void OnMouseOver()
        {
            if (_field.Mode == FieldMode.SelectObstacles)
            {
                if (Input.GetMouseButton(0))
                {
                    if (_cellState == CellState.Normal)
                        ChangeState(CellState.Blocked);
                }
                else
                if (Input.GetMouseButton(1))
                {
                    if (_cellState == CellState.Blocked)
                        ChangeState(CellState.Normal);
                }
            }
            else
            if (_field.Mode == FieldMode.SelectStartFinish)
            {
                if (Input.GetMouseButton(0))
                {
                    if (_cellState == CellState.Normal)
                        ChangeState(CellState.Start);
                }
                else
                if (Input.GetMouseButton(1))
                {
                    if (_cellState == CellState.Normal)
                        ChangeState(CellState.Finish);
                }
            }
        }

        public void ChangeState(CellState state)
        {
            _cellState = state;

            //unique cells - start and finish
            switch (state)
            {
                case CellState.Start:
                    (_field.StartNode as CellView)?.ChangeState(CellState.Normal);
                    _field.SetStartNode((INode)this);
                    break;
                case CellState.Finish:
                    (_field.FinishNode as CellView)?.ChangeState(CellState.Normal);
                    _field.SetFinishNode(this);
                    break;
            }

            //changing sprite
            switch (state)
            {
                case CellState.Normal:
                    _spriteRenderer.sprite = _cellSprites.Normal;
                break;
                case CellState.Blocked:
                    _spriteRenderer.sprite = _cellSprites.Blocked;
                break;
                case CellState.Start:
                    _spriteRenderer.sprite = _cellSprites.Start;
                    break;
                case CellState.Finish:
                    _spriteRenderer.sprite = _cellSprites.Finish;
                    break;
                case CellState.Way:
                    _spriteRenderer.sprite = _cellSprites.Way;
                    break;
            }
        }

        private void OnDrawGizmos()
        {
            foreach (var l in _links)
                Gizmos.DrawLine(l.From.GetCenter(), l.To.GetCenter());
        }

    }
}