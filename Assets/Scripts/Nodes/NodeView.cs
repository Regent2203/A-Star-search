using UnityEngine;
using Zenject;

namespace EasyField.Nodes
{
    public abstract class NodeView<TId> : MonoBehaviour, INodeView<TId>, IPoolable<TId, Vector2>
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;

        protected TId _id;
        public TId Id => _id;

        protected virtual string BasicName => "NodeView";


        public virtual void OnSpawned(TId id, Vector2 scale)
        {
            _id = id;
            transform.localScale = new Vector3(scale.x, scale.y, 1f);
            gameObject.SetActive(true);
        }

        public virtual void OnDespawned()
        {
            gameObject.SetActive(false);
        }

        
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
            transform.position = new Vector3(position.x, position.y, transform.position.z);
        }
    }
}