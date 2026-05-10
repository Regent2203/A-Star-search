using Core.Heuristic;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.SearchAlgorithms
{
    public interface ISearchAlgorithm<T> where T : class, INode
    {
        public IList<T> CalculateWay(T startNode, T finishNode, IHeuristicsProvider<T> heuristicsController);
    }
}