using UnityEngine;

namespace ThisProject.Views
{
    public abstract class View<TId> : MonoBehaviour, IView<TId>
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;

        protected TId _id;
        public TId Id => _id;


        public Vector2 GetSize()
        {
            return _spriteRenderer.bounds.size;
        }

        public Vector3 GetCenterCoords()
        {
            return _spriteRenderer.bounds.center;
        }

        public void Move(Vector2 position)
        {
            transform.position = position;
        }
    }
}