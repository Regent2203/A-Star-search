using ThisProject.Fields;
using ThisProject.Inputs;
using ThisProject.ObjectsStorages;
using UnityEngine;
using Zenject;

namespace ThisProject.Nodes
{
    public abstract class NodeView<TId> : MonoBehaviour, INodeView<TId>, IPoolable<TId, Vector2>
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;

        protected TId _id;
        public TId Id => _id;

        protected IMemoryPool _pool;


        [Inject]
        public void Construct(IMemoryPool pool)
        {
            _pool = pool;
        }

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

        public void ReturnToPool() => _pool?.Despawn(this); 

        
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