using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Implementations.Cells
{
    public class CellView : NodeView<Vector2Int>
    {
        [Space]
        [SerializeField]
        private GameObject _pathMarker;
        [SerializeField]
        private GameObject _startMarker;
        [SerializeField]
        private GameObject _finishMarker;


        public override void OnSpawned(Vector2Int index, Vector2 scale)
        {
            base.OnSpawned(index, scale);

            name = $"CellView {index.x},{index.y}";
            ClearGraphics();
        }

        public override void OnDespawned()
        {
            ClearGraphics();

            base.OnDespawned();
        }

        private void ClearGraphics()
        {
            ShowPathMarker(false);
            ShowStartMarker(false);
            ShowFinishMarker(false);
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