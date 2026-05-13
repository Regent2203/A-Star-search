using Core.Nodes;

namespace Core.CostProviders
{
    public class ConstantCostProvider<T> : ICostProvider<T> where T : class, INode
    {
        private readonly float _cost;

        public ConstantCostProvider(float cost)
        {
            _cost = cost;
        }

        public float GetCost(T from, T to) => _cost;
    }
}