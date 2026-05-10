using Core.Nodes;

namespace Core.CostProviders
{
    public interface ICostProvider<T, TId> where T : class, INode<TId>
    {
        public float GetCost(T from, T to);
    }
}