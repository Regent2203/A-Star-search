using TMPro;
using UnityEngine;
using Zenject;

namespace EasyField.Links
{
    public enum PlacementType { Center, Left, Right }    

    public class LinkView<TId> : MonoBehaviour, ILinkView<TId>, IPoolable<TId, TId, float, PlacementType>
    {
        [SerializeField]
        private float _textOffset = 2.0f;
        [SerializeField]
        private float _placementOffset = 0.5f; //offset in units for PlacementType != Center

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
        private float _arrowOffset; //sizeY of arrow tip sprite
        

        private void Awake()
        {
            _arrowOffset = _arrowTipRenderer.sprite.bounds.size.y;
        }

        public virtual void OnSpawned(TId from, TId to, float cost, PlacementType placementType)
        {
            _id = new DualKey<TId>(from, to);
            name = $"LinkView {From}->{To}";
            UpdateCostText(cost);
            _placementType = placementType;

            gameObject.SetActive(true);
        }

        public virtual void OnDespawned()
        {
            _id = default;
            name = $"LinkView";
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

        public void UpdatePositions(Vector2 posFrom, Vector2 posTo)
        {            
            var direction = (posFrom - posTo).normalized;
            var perpendicular = new Vector2(-direction.y, direction.x);

            Vector2 start, end;

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

            //todo
            Vector2 textBasePos = start - 0.6f * Vector2.Distance(start, end) * direction;
            Vector2 textPosition = textBasePos + perpendicular * _textOffset * (0.5f + Mathf.Abs(direction.y / 2.0f));

            //we have arrow tip sprite, so instead of drawing line between exactly start and end, we make line shorter and use arrow tip there
            end += direction * _arrowOffset; 

            //todo: set Z!
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;

            _arrowBodyRenderer.SetPosition(0, start);
            _arrowBodyRenderer.SetPosition(1, end);
            _arrowTipRenderer.transform.SetPositionAndRotation(end, Quaternion.Euler(0, 0, angle));

            _costText.transform.position = new Vector3(textPosition.x, textPosition.y, _costText.transform.position.z);
        }

        public void UpdateCostText(float cost)
        {
            _costText.text = $"{cost.ToString("0.00")}";
        }
    }
}