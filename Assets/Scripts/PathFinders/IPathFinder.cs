using Core.Nodes;
using System.Collections.Generic;

namespace Core.PathFinders
{
    public interface IPathFinder<T> where T : INode<T>
    {
        public void UpdateStartNode(T node);
        public void UpdateFinishNode(T node);
        public IList<T> GetPath();
    }
}