using Core.Nodes;
using UnityEngine;

namespace Core.CostProviders
{
    public class FromCostProvider<T, TId> : ICostProvider<T, TId> where T : class, INode<T, TId>
    {
        public float GetCost(T from, T to)
        {
            float baseWeight = from.Weight;
            float distance = Vector2.Distance(from.Position, to.Position);

            return baseWeight * distance;
        }
    }
}