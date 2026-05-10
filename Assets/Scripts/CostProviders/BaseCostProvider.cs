using Core.Nodes;
using UnityEngine;

namespace Core.CostProviders
{
    public abstract class BaseCostProvider<T> : ICostProvider<T> where T : class, INode
    {
        protected readonly IWeightGetter<T> _weightGetter;

        public BaseCostProvider(IWeightGetter<T> weightGetter)
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