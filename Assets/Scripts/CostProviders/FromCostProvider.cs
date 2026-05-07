using Core.Nodes;

namespace Core.CostProviders
{
    public class FromCostProvider<T> : ICostProvider<T> where T : INode<T>
    {
        public float GetCost(T from, T to)
        {
            return from.Weight;
        }
    }
}