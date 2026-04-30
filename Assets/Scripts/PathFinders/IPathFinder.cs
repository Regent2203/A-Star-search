using System.Collections.Generic;

namespace Core.PathFinders
{
    public interface IPathFinder<T>
    {
        public void UpdateStartNode(T node);
        public void UpdateFinishNode(T node);
        public IList<T> GetPath();
    }
}