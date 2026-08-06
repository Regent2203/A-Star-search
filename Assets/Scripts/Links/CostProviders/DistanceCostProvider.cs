using EasyField.Nodes;
using UnityEngine;

namespace EasyField.Links.CostProviders
{
    public class DistanceCostProvider<TNodeData> : ICostProvider<TNodeData>
        where TNodeData : INodeData
    {
        public float GetCost(TNodeData from, TNodeData to) => Vector2.Distance(from.NodePosition, to.NodePosition);
    }
}