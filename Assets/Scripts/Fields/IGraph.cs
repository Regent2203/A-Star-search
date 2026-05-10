using Core.Nodes;

namespace Core.Fields
{
    public interface IGraph<T, TId> where T : class, INode<TId>
    {
        public T GetNodeById(TId id);
    }
}