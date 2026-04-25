using Core.Nodes;

namespace Core.Links
{
    public interface ILink<T> where T : INode<T>
    {
        T From { get; }
        T To { get; }
        float Cost { get; }
    }
}
