using Core.Nodes;

namespace Core.Fields
{
    public interface IGraph<T, I> where T : INode<T>
    {
        public T GetNodeById(I id);
    }
}