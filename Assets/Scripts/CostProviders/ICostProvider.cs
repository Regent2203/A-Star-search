using Core.Nodes;

namespace Core.CostProviders
{
    public interface ICostProvider<T> where T : class, INode
    {
        public float GetCost(T from, T to);
    }
}