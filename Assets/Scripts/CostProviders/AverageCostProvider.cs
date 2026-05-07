using Core.Nodes;

namespace Core.CostProviders
{
    public class AverageCostProvider : ICostProvider
    {
        public float GetCost(INode from, INode to)
        {
            return from.Weight * 0.5f + to.Weight * 0.5f;
        }
    }
}