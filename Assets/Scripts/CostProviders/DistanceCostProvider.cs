using Core.Nodes;
using UnityEngine;

namespace Core.CostProviders
{
    public class DistanceCostProvider<T> : ICostProvider<T> where T : class, INode
    {
        public float GetCost(T from, T to) => Vector2.Distance(from.NodePosition, to.NodePosition);
    }
}