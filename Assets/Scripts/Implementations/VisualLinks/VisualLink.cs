using System.Net;
using ThisProject.Implementations.Vertexes;
using ThisProject.Links;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Implementations.VisualLinks
{
    enum PlacementType { Center, Left, Right }

    public class VisualLink : VisualLink<VertexNode>
    {
    }

    public class VisualLink<T> : MonoBehaviour 
        where T : class, INode
    {
        [SerializeField]
        private LineRenderer _arrowBodyRenderer;
        [SerializeField]
        private SpriteRenderer _arrowTipRenderer;

        private float _s;
        private ILink<T> _link;
        private PlacementType _placementType = PlacementType.Left;


        private void Awake()
        {
            _s = _arrowTipRenderer.sprite.bounds.size.y * _arrowTipRenderer.transform.localScale.y;
        }

        public void Bind(ILink<T> link)
        {
            _link = link;
            //_link.From.NodePositionChanged += OnNodePositionChanged; //todo
            //_link.To.NodePositionChanged += OnNodePositionChanged; //todo

            UpdatePositions();
        }

        private void OnNodePositionChanged(INode node, Vector2 pos)
        {
            UpdatePositions();
        }

        private void UpdatePositions()
        {
            Vector2 start, end;
            float offset = 0.5f; //todo
            
            var direction = (_link.To.NodePosition - _link.From.NodePosition).normalized;
            var perpendicular = new Vector2(-direction.y, direction.x);

            switch (_placementType)
            {
                case PlacementType.Center:
                default:
                    start = _link.From.NodePosition;
                    end = _link.To.NodePosition;
                    break;
                case PlacementType.Left:
                    start = _link.From.NodePosition + perpendicular * offset;
                    end = _link.To.NodePosition + perpendicular * offset;
                    break;

                case PlacementType.Right:
                    start = _link.From.NodePosition - perpendicular * offset;
                    end = _link.To.NodePosition - perpendicular * offset;
                    break;
            }

            var backwardShift = direction * _s; //area for arrow tip
            end -= backwardShift;

            start += 0.45f * 4 * direction; //todo: set Z!
            end -= 0.45f * 4 * direction;

            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            _arrowBodyRenderer.SetPosition(0, start);
            _arrowBodyRenderer.SetPosition(1, end);
            _arrowTipRenderer.transform.SetPositionAndRotation(end, Quaternion.Euler(0, 0, angle));
        }
    }
}