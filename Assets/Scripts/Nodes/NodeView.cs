using UnityEngine;
using Zenject;

namespace EasyField.Nodes
{
    public abstract class NodeView<TId> : MonoBehaviour, INodeView<TId>, IPoolable<TId, Vector2>
    {
        [SerializeField]
        protected SpriteRenderer _spriteRenderer;

        [Space]
        [SerializeField]
        protected GameObject _selectedMarker;
        [SerializeField]
        protected GameObject _startMarker;
        [SerializeField]
        protected GameObject _finishMarker;

        protected TId _id;
        public TId Id => _id;

        protected virtual string BasicName => "NodeView";


        public virtual void OnSpawned(TId id, Vector2 scale)
        {
            ClearGraphics();

            _id = id;
            transform.localScale = new Vector3(scale.x, scale.y, 1f);
            name = $"{BasicName} {id}";
            gameObject.SetActive(true);            
        }

        public virtual void OnDespawned()
        {
            ClearGraphics();

            _id = default;
            transform.localScale = new Vector3(1f, 1f, 1f);
            name = $"{BasicName}";
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

        protected virtual void ClearGraphics()
        {
            ShowSelectedMarker(false);
            ShowStartMarker(false);
            ShowFinishMarker(false);
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

        public void UpdateSprite(Sprite sprite)
        {
            _spriteRenderer.sprite = sprite;
        }
    }
}