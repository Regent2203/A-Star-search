using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexView : NodeView<int>
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


        public override void OnSpawned(int id, Vector2 scale)
        {
            base.OnSpawned(id, scale);

            name = $"VertexView {id}";
            ClearGraphics();
        }

        public override void OnDespawned()
        {
            ClearGraphics();
            base.OnDespawned();
        }

        private void ClearGraphics()
        {
            ShowBlockedMarker(false);
            ShowSelectedMarker(false);
            ShowStartMarker(false);
            ShowFinishMarker(false);
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