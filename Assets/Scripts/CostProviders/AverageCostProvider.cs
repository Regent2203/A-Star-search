using Core.Nodes;
using UnityEngine;

namespace Core.CostProviders
{
    public class AverageCostProvider<T> : ICostProvider<T> where T: INode<T>
    {
        public float GetCost(T from, T to)
        {
            float baseWeight = from.Weight * 0.5f + to.Weight * 0.5f;
            float distance = Vector2.Distance(from.Position, to.Position);

            return baseWeight * distance;
        }
    }
}