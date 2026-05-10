using Core.Nodes;

namespace Core.CostProviders
{
    public class FromCostProvider<T, TId> : BaseCostProvider<T, TId> where T : class, INode<TId>
    {
        public FromCostProvider(IWeightGetter<T, TId> weightGetter) : base(weightGetter)
        { }

        protected override float GetWeight(T from, T to)
        {
            return _weightGetter.GetWeight(from);
        }
    }
}