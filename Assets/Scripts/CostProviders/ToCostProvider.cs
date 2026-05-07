using Core.Nodes;

namespace Core.CostProviders
{
    public class ToCostProvider<T> : ICostProvider<T> where T: INode<T>
    {
        public float GetCost(T from, T to)
        {
            return to.Weight;
        }
    }
}