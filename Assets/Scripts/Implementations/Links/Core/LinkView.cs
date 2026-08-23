using TMPro;
using UnityEngine;
using Zenject;

namespace EasyField.Links
{
    public enum PlacementType { Center, Left, Right }    

    public class LinkView<TId> : MonoBehaviour, ILinkView<TId>, IPoolable<TId, TId, float, PlacementType>
    {
        [SerializeField]
        private BoxCollider2D _collider;

        [Header("Link")]
        [SerializeField]
        private float _linkOffsetY = 1.75f; //link offset in units for start and end point of arrow
        [Header("Text")]
        [SerializeField]
        private float _textPercentageOffsetY = 0.6f; //text offset in percents (relative to arrow length), along arrow direction (from start point)
        [SerializeField]
        private float _textOffsetX = 2.0f; //text offset in units, perpendicular to arrow direction
        [Header("Dual links")]
        [SerializeField]
        private float _spriteDualPlacementOffset = 0.5f; //sprite offset in units, perpendicular to arrow direction, for PlacementType != Center

        [Space]
        [SerializeField]
        private TextMeshPro _costText;
        [SerializeField]
        private LineRenderer _arrowBodyRenderer;
        [SerializeField]
        private SpriteRenderer _arrowTipRenderer;

        protected DualKey<TId> _id;

        public DualKey<TId> Id => _id;
        public TId From => _id.From;
        public TId To => _id.To;

        private PlacementType _placementType = PlacementType.Center;
        private float _arrowTipOffsetY; //sizeY of arrow tip sprite

        protected virtual string BasicName => "LinkView";


        private void Awake()
        {
            _arrowTipOffsetY = _arrowTipRenderer.sprite.bounds.size.y;
        }

        public virtual void OnSpawned(TId from, TId to, float cost, PlacementType placementType)
        {
            _id = new DualKey<TId>(from, to);
            name = $"{BasicName} {From}->{To}";
            UpdateCostText(cost);
            _placementType = placementType;

            gameObject.SetActive(true);
        }

        public virtual void OnDespawned()
        {
            _id = default;
            name = $"{BasicName}";
            UpdateCostText(0.0f);
            _placementType = PlacementType.Center;

            gameObject.SetActive(false);
        }

        public void ChangePlacementType(PlacementType placementType)
        {
            if (_placementType == placementType)
                return;

            _placementType = placementType;
        }

        public void UpdateCostText(float cost)
        {
            _costText.text = $"{cost.ToString("0.00")}";
        }

        public void UpdatePositions(Vector2 posFrom, Vector2 posTo)
        {            
            var direction = (posFrom - posTo).normalized;
            var perpendicular = new Vector2(-direction.y, direction.x);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            Vector2 start, end;

            switch (_placementType)
            {
                case PlacementType.Center:
                default:
                    start = posFrom;
                    end = posTo;
                    break;
                case PlacementType.Left:
                    start = posFrom + perpendicular * _spriteDualPlacementOffset;
                    end = posTo + perpendicular * _spriteDualPlacementOffset;
                    break;

                case PlacementType.Right:
                    start = posFrom - perpendicular * _spriteDualPlacementOffset;
                    end = posTo - perpendicular * _spriteDualPlacementOffset;
                    break;
            }

            start += -direction * _linkOffsetY;
            end -= -direction * _linkOffsetY;

            //we have arrow tip sprite, so instead of drawing line between exactly start and end, we make line shorter and use arrow tip there
            end -= -direction * _arrowTipOffsetY;


            //arrow
            var arrowAngle = angle + 90f;

            _arrowBodyRenderer.SetPosition(0, start - (Vector2)_arrowBodyRenderer.transform.position);
            _arrowBodyRenderer.SetPosition(1, end - (Vector2)_arrowBodyRenderer.transform.position);
            _arrowTipRenderer.transform.SetPositionAndRotation(end, Quaternion.Euler(0, 0, arrowAngle));


            //text
            var textBasePos = start - _textPercentageOffsetY * Vector2.Distance(start, end) * direction; //centered on arrow line
            var textPosition = textBasePos + (0.5f + Mathf.Abs(direction.y / 2.0f)) * _textOffsetX * perpendicular; //offsetted to the side

            _costText.transform.position = new Vector3(textPosition.x, textPosition.y, _costText.transform.position.z);


            //collider
            var distance = Vector2.Distance(start, end);
            var centerPosition = Vector2.Lerp(start, end, 0.5f);            
            var collPos = new Vector3(centerPosition.x, centerPosition.y, 0);

            _collider.transform.SetPositionAndRotation(collPos, Quaternion.Euler(0, 0, angle));
            _collider.size = new Vector2(distance, _collider.size.y);
        }
    }
}