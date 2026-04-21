using System.Collections.Generic;

namespace Core.PathFinders
{
    public interface IPathFinder<T>
    {
        public void SetStartNode(T node);
        public void SetFinishNode(T node);
        public IList<T> GetPath();
    }
}