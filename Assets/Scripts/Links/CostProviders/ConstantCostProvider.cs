using EasyField.Nodes;

namespace EasyField.Links.CostProviders
{
    public class ConstantCostProvider<TNodeData> : ICostProvider<TNodeData>
        where TNodeData : INodeData
    {
        private readonly float _cost;

        public ConstantCostProvider(float cost)
        {
            _cost = cost;
        }

        public float GetCost(TNodeData from, TNodeData to) => _cost;
    }
}