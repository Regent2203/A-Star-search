using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.PathFinders
{
    public interface IPathFinder<T>
        where T : INodeData
    {
        public IList<T> GetPath(T startNode, T finishNode);
    }
}