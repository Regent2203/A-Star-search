using Core.Nodes;

namespace Core.CostProviders
{
    public class TargetCostProvider<T> : ICostProvider<T> where T: INode<T>
    {
        public float GetCost(T from, T to)
        {
            return to.Weight;
        }
    }
}