using Core.Nodes;

namespace Core.CostProviders
{
    public class AverageCostProvider<T> : ICostProvider<T> where T: INode<T>
    {
        public float GetCost(T from, T to)
        {
            return from.Weight * 0.5f + to.Weight * 0.5f;
        }
    }
}