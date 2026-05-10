using Core.Nodes;
using System.Collections.Generic;

namespace Core.PathFinders
{
    public interface IPathFinder<T> where T : class, INode
    {
        public void UpdateStartNode(T node);
        public void UpdateFinishNode(T node);
        public IList<T> GetPath();
    }
}