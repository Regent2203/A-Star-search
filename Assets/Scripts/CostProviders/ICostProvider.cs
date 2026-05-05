using Core.Weightables;

namespace Core.CostProviders
{
    public interface ICostProvider
    {
        float GetCost(IWeightable from, IWeightable to);
    }
}
