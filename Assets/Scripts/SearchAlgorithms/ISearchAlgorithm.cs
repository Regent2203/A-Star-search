using ThisProject.Heuristic;
using ThisProject.Links.Providers;
using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.SearchAlgorithms
{
    public interface ISearchAlgorithm<T, TId>
        where T : INodeData<TId>
    {
        public IList<T> CalculateWay(T startNode, T finishNode, IHeuristicsProvider<T> heuristicsController, ILinksProvider<T, TId> linksProvider);
    }
}