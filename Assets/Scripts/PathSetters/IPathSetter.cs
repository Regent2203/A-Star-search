using ThisProject.Nodes;

namespace ThisProject.PathSetters
{
    public interface IPathSetter<T> where T : class, INode
    {
        public void UpdateStartNode(T node);
        public void UpdateFinishNode(T node);
    }
}