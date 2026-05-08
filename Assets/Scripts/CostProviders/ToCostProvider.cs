using Core.Nodes;
using UnityEngine;

namespace Core.CostProviders
{
    public class ToCostProvider<T> : ICostProvider<T> where T: INode<T>
    {
        public float GetCost(T from, T to)
        {
            float baseWeight = to.Weight;
            float distance = Vector2.Distance(from.Position, to.Position);

            return baseWeight * distance;
        }
    }
}