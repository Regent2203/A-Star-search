using EasyField.Nodes;

namespace EasyField.PathSetters
{
    public interface IPathSetter<T>
        where T : INodeData
    {
        public void UpdateStartNode(T node);
        public void UpdateFinishNode(T node);
    }
}