using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexView : View<int>
    {
        [SerializeField]
        private CircleCollider2D _collider;
        [SerializeField]
        private GameObject _startMarker;
        [SerializeField]
        private GameObject _finishMarker;


        private void Awake()
        {
            ShowStartMarker(false);
            ShowFinishMarker(false);
        }

        public void Init(int id, Vector2 position)
        {
            _id = id;
            name = $"VertexView {id}";
            transform.position = position;
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