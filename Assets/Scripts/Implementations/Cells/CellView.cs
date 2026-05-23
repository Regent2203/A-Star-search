using Core.Views;
using UnityEngine;

namespace Core.Implementations.Cells
{
    public class CellView : MonoBehaviour, IView<Vector2Int>
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
        public Vector2Int Id => _index;

        private Vector2? _size;
        private Vector3? _center;


        private void Awake()
        {
            ShowPathMarker(false);
            ShowStartMarker(false);
            ShowFinishMarker(false);
        }

        public void Init(Vector2Int index, Vector2 scale)
        {
            _index = index;
            transform.localScale = scale;
            name = $"Cell {index.x},{index.y}";
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
        public void UpdateSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public Vector2 GetSize()
        {
            _size ??= _spriteRenderer.size;
            return _size.Value;
        }

        public Vector3 GetCenterCoords()
        {
            _center ??= _spriteRenderer.bounds.center;
            return _center.Value;
        }
    }
}