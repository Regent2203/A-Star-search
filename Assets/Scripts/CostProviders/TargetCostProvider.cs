using Core.Weightables;

namespace Core.CostProviders
{
    public class TargetCostProvider : ICostProvider
    {
        public float GetCost(IWeightable from, IWeightable to)
        {
            return to.Weight;
        }
    }
}
