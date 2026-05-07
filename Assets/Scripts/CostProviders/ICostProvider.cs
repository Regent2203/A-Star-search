using Core.Nodes;

namespace Core.CostProviders
{
    public interface ICostProvider<T> where T : INode<T>
    {
        float GetCost(T from, T to);
    }
}