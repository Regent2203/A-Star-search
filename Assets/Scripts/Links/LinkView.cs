using ThisProject.Nodes;
using UnityEngine;
using Zenject;

namespace ThisProject.Links
{
    public enum PlacementType { Center, Left, Right }    

    public class LinkView<TId> : MonoBehaviour, ILinkView<TId>, IPoolable<INodeView<TId>, INodeView<TId>, PlacementType>
    {
        [SerializeField]
        private float _placementOffset = 0.5f;

        [Space]
        [SerializeField]
        private LineRenderer _arrowBodyRenderer;
        [SerializeField]
        private SpriteRenderer _arrowTipRenderer;

        protected LinkKey<TId> _id;
        protected INodeView<TId> _viewFrom, _viewTo;

        public LinkKey<TId> Id => _id; //todo remove? dual source?
        public TId From => _viewFrom.Id;
        public TId To => _viewTo.Id;

        private PlacementType _placementType = PlacementType.Center;
        private float _arrowOffset; //sizeY of arrow tip sprite
        

        private void Awake()
        {
            _arrowOffset = _arrowTipRenderer.sprite.bounds.size.y;
        }

        public virtual void OnSpawned(INodeView<TId> viewFrom, INodeView<TId> viewTo, PlacementType placementType)
        {
            _viewFrom = viewFrom;
            _viewTo = viewTo;

            //_id = new LinkKey<TId>(viewFrom.Id, viewTo.Id);
            name = $"LinkView {From}->{To}";
            _placementType = placementType;
            UpdatePositions();

            gameObject.SetActive(true);
        }

        public virtual void OnDespawned()
        {
            _viewFrom = null;
            _viewTo = null;

            gameObject.SetActive(false);
        }

        public void ChangePlacementType(PlacementType placementType)
        {
            if (_placementType == placementType)
                return;

            _placementType = placementType;

            UpdatePositions();
        }

        public void UpdatePositions()
        {
            Vector2 posFrom = _viewFrom.GetCenterCoords();
            Vector2 posTo = _viewTo.GetCenterCoords();
            
            Vector2 start, end;
            
            var direction = (posFrom - posTo).normalized;
            var perpendicular = new Vector2(-direction.y, direction.x);

            switch (_placementType)
            {
                case PlacementType.Center:
                default:
                    start = posFrom;
                    end = posTo;
                    break;
                case PlacementType.Left:
                    start = posFrom + perpendicular * _placementOffset;
                    end = posTo + perpendicular * _placementOffset;
                    break;

                case PlacementType.Right:
                    start = posFrom - perpendicular * _placementOffset;
                    end = posTo - perpendicular * _placementOffset;
                    break;
            }

            //we have arrow tip sprite, so instead of drawing line between exactly start and end, we make line shorter and use arrow tip there
            end += direction * _arrowOffset; 

            //todo: set Z!
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

            _arrowBodyRenderer.SetPosition(0, start);
            _arrowBodyRenderer.SetPosition(1, end);
            _arrowTipRenderer.transform.SetPositionAndRotation(end, Quaternion.Euler(0, 0, angle));
        }
    }
}