using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    public class CellView : View<Vector2Int>
    {
        [SerializeField]
        private GameObject _pathMarker;
        [SerializeField]
        private GameObject _startMarker;
        [SerializeField]
        private GameObject _finishMarker;

        
        private void Awake()
        {
            ShowPathMarker(false);
            ShowStartMarker(false);
            ShowFinishMarker(false);
        }

        public void Init(Vector2Int index, Vector2 scale)
        {
            _id = index;
            transform.localScale = scale;
            name = $"CellView {index.x},{index.y}";
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
    }
}