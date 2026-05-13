using Core.Nodes;

namespace Core.Links.Factories.CostProviders
{
    public interface ICostProvider<T> where T : class, INode
    {
        public float GetCost(T from, T to);
    }
}