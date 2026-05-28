using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.PathFinders
{
    public interface IPathFinder<T> where T : class, INode
    {
        public IList<T> GetPath(T startNode, T finishNode);
    }
}