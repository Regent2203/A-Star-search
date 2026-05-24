using ThisProject.Nodes;

namespace ThisProject.Links
{
    public interface ILink<T> where T : class, INode
    {
        public T From { get; }
        public T To { get; }
        public float Cost { get; }

        public void ChangeCost(float value);
    }
}