using Core.Nodes;

namespace Core.Links
{
    public interface ILink<T, TId> where T : class, INode<T, TId>
    {
        public T From { get; }
        public T To { get; }
        public float Cost { get; }
    }
}