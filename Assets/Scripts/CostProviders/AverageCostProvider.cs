using Core.Nodes;

namespace Core.CostProviders
{
    public class AverageCostProvider<T, TId> : BaseCostProvider<T, TId> where T : class, INode<TId>
    {
        public AverageCostProvider(IWeightGetter<T, TId> weightGetter) : base(weightGetter) 
        { }

        protected override float GetWeight(T from, T to)
        {
            return (_weightGetter.GetWeight(from) + _weightGetter.GetWeight(to)) * 0.5f;
        }
    }
}