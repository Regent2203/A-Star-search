using EasyField.Nodes;

namespace EasyField.PathSetters
{
    public interface IPathSetter<TNodeData>
        where TNodeData : INodeData
    {
        public void UpdateStartNode(TNodeData nodeData);
        public void UpdateFinishNode(TNodeData nodeData);
    }
}