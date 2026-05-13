using Core.Nodes;

namespace Core.CostProviders
{
    public class AverageCostProvider<T> : WeightedCostProvider<T> where T : class, INode
    {
        public AverageCostProvider(IWeightGetter<T> weightGetter) : base(weightGetter) 
        { }

        protected override float GetWeight(T from, T to)
        {
            return (_weightGetter.GetWeight(from) + _weightGetter.GetWeight(to)) * 0.5f;
        }
    }
}