using System;
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

        private T _startNode;
        private T _finishNode;

        public event Action<bool> AnyNodeChanged;
        public event Action<T, bool> StartNodeChanged;  //false is called when cleared, true is called when assigned
        public event Action<T, bool> FinishNodeChanged; //false is called when cleared, true is called when assigned
        public bool IsReady => _startNode != null && _finishNode != null;


        public PathFinder(IHeuristicsProvider<T> heuristicFunction, ILinksProvider<T> linksProvider, ISearchAlgorithm<T> searchAlgorithm)
        {
            _heuristicsProvider = heuristicFunction;
            _linksProvider = linksProvider;
            _searchAlgorithm = searchAlgorithm;
        }

        public void UpdateStartNode(T node)
        {
            UpdateDesiredNode(node, ref _startNode, ref _finishNode, StartNodeChanged);
        }

        public void UpdateFinishNode(T node)
        {
            UpdateDesiredNode(node, ref _finishNode, ref _startNode, FinishNodeChanged);
        }

        private void UpdateDesiredNode(T node, ref T desiredNode, ref T notDesiredNode, Action<T, bool> desiredNodeChanged)
        {
            if (node is not null && ReferenceEquals(notDesiredNode, node)) //when trying to set start node as finish node or vice versa
                return;

            if (node is null && desiredNode is null) //when trying to set null to null
                return;

            if (ReferenceEquals(desiredNode, node)) //when trying to set same value to same desyred node, we clear desyred value instead (it's a feature)
            {
                var oldDesiredNode = desiredNode;
                desiredNode = null;
                desiredNodeChanged?.Invoke(oldDesiredNode, false);
                AnyNodeChanged?.Invoke(IsReady);
                return;
            }

            if (desiredNode is not null) //if desired node is already set, we should clear the previous one
            {
                desiredNodeChanged?.Invoke(desiredNode, false);
            }
            desiredNode = node;
            desiredNodeChanged?.Invoke(desiredNode, true);

            AnyNodeChanged?.Invoke(IsReady);
        }

        public IList<T> GetPath()
        {
            if (_startNode == null || _finishNode == null) 
                return null;

            return _searchAlgorithm.CalculateWay(_startNode, _finishNode, _heuristicsProvider, _linksProvider);
        }
    }
}
