using ThisProject.Heuristic;
using ThisProject.Links.Providers;
using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.SearchAlgorithms
{
    public interface ISearchAlgorithm<T>
        where T : INodeData
    {
        public IList<T> CalculateWay(T startNode, T finishNode, IHeuristicsProvider<T> heuristicsController, ILinksProvider<T> linksProvider);
    }
}