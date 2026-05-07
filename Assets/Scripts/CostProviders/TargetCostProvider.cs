using Core.Nodes;

namespace Core.CostProviders
{
    public class TargetCostProvider : ICostProvider
    {
        public float GetCost(INode from, INode to)
        {
            return to.Weight;
        }
    }
}