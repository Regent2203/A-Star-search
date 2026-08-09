using Zenject;

namespace EasyField.Links
{
    public class LinkData<TId> : ILinkData<TId>, IPoolable<TId, TId, float>
    {
        protected DualKey<TId> _id;
        protected float _cost;

        public DualKey<TId> Id => _id;
        public TId From => _id.From;
        public TId To => _id.To;
        public float Cost => _cost;
        

        public virtual void OnSpawned(TId fromId, TId toId, float cost)
        {
            _id = new DualKey<TId>(fromId, toId);
            _cost = cost;
        }

        public virtual void OnDespawned()
        {
            _id = default;
            _cost = 0;
        }

        public void SetCost(float value)
        {
            _cost = value;
        }
    }
}