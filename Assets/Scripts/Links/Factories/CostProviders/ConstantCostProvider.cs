using ThisProject.Nodes;

namespace ThisProject.Links.Factories.CostProviders
{
    public class ConstantCostProvider<T> : ICostProvider<T>
        where T : INodeData
    {
        private readonly float _cost;

        public ConstantCostProvider(float cost)
        {
            _cost = cost;
        }

        public float GetCost(T from, T to) => _cost;
    }
}