using System;
using System.Collections.Generic;
using Core.Heuristic;
using Core.Nodes;
using Core.SearchAlgorithms;

namespace Core.PathFinders
{
    public class PathFinder<T> : IPathFinder<T> where T : INode<T>
    {
        private IHeuristicsProvider _heuristicsProvider;
        private ISearchAlgorithm<T> _searchAlgorithm;

        private T _startNode;
        private T _finishNode;

        public event Action NodeChanged;
        public event Action<T, bool> StartNodeChanged;  //false is called when cleared, true is called when assigned
        public event Action<T, bool> FinishNodeChanged; //false is called when cleared, true is called when assigned
        public event Action<bool> StartAndFinishReady;


        public PathFinder(IHeuristicsProvider heuristicFunction, ISearchAlgorithm<T> searchAlgorithm)
        {
            _heuristicsProvider = heuristicFunction;
            _searchAlgorithm = searchAlgorithm;
        }

        public void SetStartNode(T node)
        {
            if (!EqualityComparer<T>.Default.Equals(node, default) && EqualityComparer<T>.Default.Equals(_finishNode, node)) //trying to set finish node as start node
                return;

            if (EqualityComparer<T>.Default.Equals(node, default) && EqualityComparer<T>.Default.Equals(_startNode, default)) //null to null
                return;

            if (EqualityComparer<T>.Default.Equals(_startNode, node)) //if same node, we clear it instead
            {
                var startNode = _startNode;
                _startNode = default;
                StartNodeChanged?.Invoke(startNode, false);
                NodeChanged?.Invoke();
                return;
            }

            if (!EqualityComparer<T>.Default.Equals(_startNode, default)) //if start node is already set, we should clear the previous one
            {
                StartNodeChanged?.Invoke(_startNode, false);
            }
            _startNode = node;
            StartNodeChanged?.Invoke(_startNode, true);
            NodeChanged?.Invoke();
            CheckStartAndFinishReady();
        }

        public void SetFinishNode(T node)
        {
            if (!EqualityComparer<T>.Default.Equals(node, default) && EqualityComparer<T>.Default.Equals(_startNode, node)) //trying to set start node as finish node
                return;

            if (EqualityComparer<T>.Default.Equals(node, default) && EqualityComparer<T>.Default.Equals(_finishNode, default)) //null to null
                return;

            if (EqualityComparer<T>.Default.Equals(_finishNode, node)) //if same node, we clear it instead
            {
                var finishNode = _finishNode;
                _finishNode = default;
                FinishNodeChanged?.Invoke(finishNode, false);
                NodeChanged?.Invoke();
                return;
            }

            if (!EqualityComparer<T>.Default.Equals(_finishNode, default)) //if finish node is already set, we should clear the previous one
            {
                FinishNodeChanged?.Invoke(_finishNode, false);
            }
            _finishNode = node;
            FinishNodeChanged?.Invoke(_finishNode, true);
            NodeChanged?.Invoke();
            CheckStartAndFinishReady();
        }

        public void CheckStartAndFinishReady()
        {
            StartAndFinishReady?.Invoke(_startNode != null && _finishNode != null);
        }

        public IList<T> GetPath()
        {
            if (_startNode == null || _finishNode == null) 
                return null;

            return _searchAlgorithm.CalculateWay(_startNode, _finishNode, _heuristicsProvider);
        }
    }
}
