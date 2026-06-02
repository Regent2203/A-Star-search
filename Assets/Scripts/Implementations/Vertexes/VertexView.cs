using ThisProject.Views;
using UnityEngine;

namespace ThisProject.Implementations.Vertexes
{
    public class VertexView : MonoBehaviour, IView<int>
    {
        [SerializeField]
        private GameObject _startMarker;
        [SerializeField]
        private GameObject _finishMarker;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private int _id;
        public int Id => _id;


        private void Awake()
        {
            //todo
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


        public Vector2 GetSize()
        {
            return _spriteRenderer.size;
        }

        public Vector3 GetCenterCoords()
        {
            return _spriteRenderer.bounds.center;
        }
    }
}