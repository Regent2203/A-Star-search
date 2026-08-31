using Zenject;

namespace EasyField.Links
{
    public class LinkData<TId> : ILinkData<TId>, IPoolable<TId, TId, float>
    {
        protected DualKey<TId> _id;
        protected float _cost;
        protected bool _isBlocked;

        public DualKey<TId> Id => _id;
        public TId From => _id.From;
        public TId To => _id.To;
        public float Cost => _cost;

        public bool IsBlocked => _isBlocked;

        public virtual void OnSpawned(TId fromId, TId toId, float cost)
        {
            _id = new DualKey<TId>(fromId, toId);
            _cost = cost;
            _isBlocked = false;
        }

        public virtual void OnDespawned()
        {
            _id = default;
            _cost = 0;
            _isBlocked = false;
        }

        public void SetCost(float value)
        {
            _cost = value;
        }

        public bool TrySetBlocked(bool blocked)
        {
            if (blocked != _isBlocked)
            {
                _isBlocked = blocked;
                return true;
            }
            return false;
        }
    }
}