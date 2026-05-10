using Core.Nodes;

namespace Core.CostProviders
{
    public class ToCostProvider<T, TId> : BaseCostProvider<T, TId> where T : class, INode<TId>
    {
        public ToCostProvider(IWeightGetter<T> weightGetter) : base(weightGetter)
        { }

        protected override float GetWeight(T from, T to)
        {
            return _weightGetter.GetWeight(to);
        }
    }
}