using Core.Nodes;
using UnityEngine;

namespace Core.CostProviders
{
    public abstract class BaseCostProvider<T, TId> : ICostProvider<T, TId> where T : class, INode<TId>
    {
        protected readonly IWeightGetter<T, TId> _weightGetter;

        public BaseCostProvider(IWeightGetter<T, TId> weightGetter)
        {
            _weightGetter = weightGetter;
        }

        protected abstract float GetWeight(T from, T to);

        public float GetCost(T from, T to)
        {
            return GetWeight(from, to) * Vector2.Distance(from.NodePosition, to.NodePosition);
        }
    }
}