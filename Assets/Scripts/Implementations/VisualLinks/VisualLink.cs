using ThisProject.Implementations.Vertexes;
using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Implementations.VisualLinks
{
    public enum PlacementType { Center, Left, Right }

    public class VisualLink : VisualLink<VertexNode>
    {
    }

    public class VisualLink<T> : MonoBehaviour 
        where T : class, INodeData
    {
        [SerializeField]
        private float _placementOffset = 0.5f;

        [Space]
        [SerializeField]
        private LineRenderer _arrowBodyRenderer;
        [SerializeField]
        private SpriteRenderer _arrowTipRenderer;

        private int _id;
        private INodeView _viewFrom, _viewTo;

        private PlacementType _placementType = PlacementType.Center;
        private float _arrowOffset;
        

        public int Id => _id;


        private void Awake()
        {
            _arrowOffset = _arrowTipRenderer.sprite.bounds.size.y * _arrowTipRenderer.transform.localScale.y;
        }

        public void Init(int id, INodeView viewFrom, INodeView viewTo, PlacementType placementType)
        {
            _id = id;
            _viewFrom = viewFrom;
            _viewTo = viewTo;
            _placementType = placementType;

            Redraw();
        }

        public void ChangePlacementType(PlacementType placementType)
        {
            if (_placementType == placementType)
                return;

            _placementType = placementType;

            Redraw();
        }

        private void Redraw()
        {
            Vector2 vFrom = _viewFrom.GetCenterCoords();
            Vector2 vTo = _viewTo.GetCenterCoords();
            
            Vector2 start, end;
            
            var direction = (vFrom - vTo).normalized;
            var perpendicular = new Vector2(-direction.y, direction.x);

            switch (_placementType)
            {
                case PlacementType.Center:
                default:
                    start = vFrom;
                    end = vTo;
                    break;
                case PlacementType.Left:
                    start = vFrom + perpendicular * _placementOffset;
                    end = vTo + perpendicular * _placementOffset;
                    break;

                case PlacementType.Right:
                    start = vFrom - perpendicular * _placementOffset;
                    end = vTo - perpendicular * _placementOffset;
                    break;
            }

            end -= direction * _arrowOffset; //area for arrow tip

            //start += 0.45f * 4 * direction;
            //end -= 0.45f * 4 * direction;

            //todo: set Z!
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            _arrowBodyRenderer.SetPosition(0, start);
            _arrowBodyRenderer.SetPosition(1, end);
            _arrowTipRenderer.transform.SetPositionAndRotation(end, Quaternion.Euler(0, 0, angle));
        }
    }
}