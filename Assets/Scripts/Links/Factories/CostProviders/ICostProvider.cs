using ThisProject.Nodes;

namespace ThisProject.Links.Factories.CostProviders
{
    public interface ICostProvider<T> where T : class, INode
    {
        public float GetCost(T from, T to);
    }
}