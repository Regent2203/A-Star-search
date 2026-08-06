using EasyField.Nodes;
using System.Collections.Generic;

namespace EasyField.PathFinders
{
    public interface IPathFinder<TNodeData>
        where TNodeData : INodeData
    {
        public IList<TNodeData> GetPath(TNodeData startNode, TNodeData finishNode);
    }
}