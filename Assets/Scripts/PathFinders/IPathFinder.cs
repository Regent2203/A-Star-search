using EasyField.Nodes;
using System.Collections.Generic;

namespace EasyField.PathFinders
{
    public interface IPathFinder<T>
        where T : INodeData
    {
        public IList<T> GetPath(T startNode, T finishNode);
    }
}