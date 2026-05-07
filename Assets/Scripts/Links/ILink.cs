using Core.Nodes;

namespace Core.Links
{
    public interface ILink
    {
        public INode From { get; }
        public INode To { get; }
        public float Cost { get; }
    }
}