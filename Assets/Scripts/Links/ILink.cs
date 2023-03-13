using Algorithm;

namespace Links
{
    public interface ILink
    {
        INode From { get; }
        INode To { get; }
        float Cost { get; }
    }
}
