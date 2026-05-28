using System.Collections.Generic;
using ThisProject.Heuristic;
using ThisProject.Links.Providers;
using ThisProject.Nodes;
using ThisProject.SearchAlgorithms;

namespace ThisProject.PathFinders
{
    public class PathFinder<T> : IPathFinder<T> where T : class, INode
    {
        private readonly IHeuristicsProvider<T> _heuristicsProvider;
        private readonly ILinksProvider<T> _linksProvider;
        private readonly ISearchAlgorithm<T> _searchAlgorithm;


        public PathFinder(IHeuristicsProvider<T> heuristicFunction, ILinksProvider<T> linksProvider, ISearchAlgorithm<T> searchAlgorithm)
        {
            _heuristicsProvider = heuristicFunction;
            _linksProvider = linksProvider;
            _searchAlgorithm = searchAlgorithm;
        }

        public IList<T> GetPath(T startNode, T finishNode)
        {
            if (startNode == null || finishNode == null) 
                return null;

            return _searchAlgorithm.CalculateWay(startNode, finishNode, _heuristicsProvider, _linksProvider);
        }
    }
}
