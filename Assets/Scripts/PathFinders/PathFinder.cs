using System;
using System.Collections.Generic;
using Core.HeuristicFunctions;
using Core.Nodes;
using Core.SearchAlgorithms;

namespace Core.PathFinders
{
    public class PathFinder<T> : IPathFinder<T> where T : INode<T>
    {
        private IHeuristicFunction _heuristicFunction;
        private ISearchAlgorithm<T> _searchAlgorithm;

        private T _startNode;
        private T _finishNode;
        
        public event Action<T, bool> StartNodeSet;
        public event Action<T, bool> FinishNodeSet;
        public event Action<bool> StartAndFinishReady;


        public PathFinder(IHeuristicFunction heuristicFunction, ISearchAlgorithm<T> searchAlgorithm)
        {
            _heuristicFunction = heuristicFunction;
            _searchAlgorithm = searchAlgorithm;

            StartNodeSet += (_,_) => CheckStartAndFinishReady();
            FinishNodeSet += (_,_) => CheckStartAndFinishReady();
        }

        public void SetStartNode(T newNode)
        {
            if (EqualityComparer<T>.Default.Equals(_startNode, newNode))
                return;

            var oldNode = _startNode;
            _startNode = newNode;

            StartNodeSet?.Invoke(oldNode, false);
            StartNodeSet?.Invoke(newNode, true);
        }

        public void SetFinishNode(T newNode)
        {
            if (EqualityComparer<T>.Default.Equals(_finishNode, newNode))
                return;

            var oldNode = _finishNode;
            _finishNode = newNode;

            FinishNodeSet?.Invoke(oldNode, false);
            FinishNodeSet?.Invoke(newNode, true);
        }

        protected void CheckStartAndFinishReady()
        {
            StartAndFinishReady?.Invoke(_startNode != null && _finishNode != null);
        }

        public IList<T> GetPath()
        {
            return _searchAlgorithm.CalculateWay(_startNode, _finishNode, _heuristicFunction);
        }
    }
}
