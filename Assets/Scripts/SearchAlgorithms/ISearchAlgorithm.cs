using System.Collections.Generic;
using ThisProject.Nodes;

namespace ThisProject.SearchAlgorithms
{
    public interface ISearchAlgorithm<T>
        where T : INodeData
    {
        public IList<T> CalculateWay(T startNode, T finishNode);
    }
}