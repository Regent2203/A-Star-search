using Core.Nodes;

namespace Core.CostProviders
{
    public interface ICostProvider<T, TId> where T : class, INode<T, TId>
    {
        public float GetCost(T from, T to);
    }
}