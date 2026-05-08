using Core.Nodes;

namespace Core.Fields
{
    public interface IGraph<T, TId> where T : class, INode<T, TId>
    {
        public T GetNodeById(TId id);
    }
}