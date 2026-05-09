using Core.Nodes;

namespace Core.CostProviders
{
    public interface IWeightGetter<T, TId> where T : class, INode<T, TId>
    {
        public float GetWeight(T node);
    }    
}