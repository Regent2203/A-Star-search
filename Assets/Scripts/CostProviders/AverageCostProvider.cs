using Core.Weightables;

namespace Core.CostProviders
{
    public class AverageCostProvider : ICostProvider
    {
        public float GetCost(IWeightable from, IWeightable to)
        {
            return from.Weight * 0.5f + to.Weight * 0.5f;
        }
    }
}