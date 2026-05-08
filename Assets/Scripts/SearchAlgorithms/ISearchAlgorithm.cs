using Core.Heuristic;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.SearchAlgorithms
{
    public interface ISearchAlgorithm<T, TId> where T : class, INode<T, TId>
    {
        public IList<T> CalculateWay(T startNode, T finishNode, IHeuristicsProvider<T, TId> heuristicsController);
    }
}