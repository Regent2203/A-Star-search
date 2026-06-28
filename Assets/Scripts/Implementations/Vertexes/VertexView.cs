using ThisProject.Views;
using UnityEngine;
using UnityEngine.UIElements;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexView : View<int>
    {
        [SerializeField]
        private CircleCollider2D _collider;

        [Space]
        [SerializeField]
        private GameObject _blockedMarker;
        [SerializeField]
        private GameObject _selectedMarker;
        [SerializeField]
        private GameObject _startMarker;
        [SerializeField]
        private GameObject _finishMarker;


        private void Awake()
        {
            ClearGraphics();
        }

        private void ClearGraphics()
        {
            ShowBlockedMarker(false);
            ShowSelectedMarker(false);
            ShowStartMarker(false);
            ShowFinishMarker(false);
        }

        public void Init(int id, Vector2 scale)
        {
            _id = id;
            transform.localScale = scale;
            name = $"VertexView {id}";
        }

        public void ShowBlockedMarker(bool show)
        {
            _blockedMarker.SetActive(show);
        }
        public void ShowSelectedMarker(bool show)
        {
            _selectedMarker.SetActive(show);
        }
        public void ShowStartMarker(bool show)
        {
            _startMarker.SetActive(show);
        }
        public void ShowFinishMarker(bool show)
        {
            _finishMarker.SetActive(show);
        }
    }
}