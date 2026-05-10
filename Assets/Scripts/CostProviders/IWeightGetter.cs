using Core.Nodes;

namespace Core.CostProviders
{
    public interface IWeightGetter<T, TId> where T : class, INode<TId>
    {
        public float GetWeight(T node);
    }    
}