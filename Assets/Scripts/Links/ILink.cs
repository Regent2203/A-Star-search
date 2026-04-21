using Core.Nodes;

namespace Core.Links
{
    public interface ILink<T>
    {
        T From { get; }
        T To { get; }
        float Cost { get; }
    }
}
