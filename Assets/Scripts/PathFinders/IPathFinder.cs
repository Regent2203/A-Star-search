using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.PathFinders
{
    public interface IPathFinder<T> where T : class, INode
    {
        public void UpdateStartNode(T node);
        public void UpdateFinishNode(T node);
        public IList<T> GetPath();
    }
}