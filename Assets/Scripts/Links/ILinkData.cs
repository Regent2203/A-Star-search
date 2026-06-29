using ThisProject.Nodes;

namespace ThisProject.Links
{
    public interface ILinkData<T>
        where T : INodeData
    {
        public T From { get; }
        public T To { get; }
        public float Cost { get; }

        public void ChangeCost(float value);
    }
}