using UnityEngine;

namespace ThisProject.Nodes
{
    public abstract class NodeView<TId> : MonoBehaviour, INodeView<TId>
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