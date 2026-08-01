using ThisProject.Nodes;
using UnityEngine;

namespace ThisProject.Links.CostProviders
{
    public abstract class WeightedCostProvider<TNodeData> : ICostProvider<TNodeData>
        where TNodeData : INodeData
    {
        protected readonly IWeightGetter<TNodeData> _weightGetter;

        public WeightedCostProvider(IWeightGetter<TNodeData> weightGetter)
        {
            _weightGetter = weightGetter;
        }

        protected abstract float GetWeight(TNodeData from, TNodeData to);

        public float GetCost(TNodeData from, TNodeData to)
        {
            return GetWeight(from, to) * Vector2.Distance(from.NodePosition, to.NodePosition);
        }
    }
}