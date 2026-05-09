using Core.Views;
using UnityEngine;

namespace Core.Implementations.Vertexes
{
    public class VertexView : MonoBehaviour, IView
    {
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
            name = $"Vertex {id}";
            transform.position = position;
        }

        public void UpdateSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }

        public Vector2 GetSize() => _spriteRenderer.size;
        public Vector3 GetCenterCoords() => _spriteRenderer.bounds.center;
    }
}