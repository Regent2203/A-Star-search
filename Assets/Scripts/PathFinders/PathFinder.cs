using System;
using System.Collections.Generic;
using Core.HeuristicFunctions;
using Core.Nodes;
using Core.SearchAlgorithms;
using UnityEngine;

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

        public void SetStartNode(T node)
        {
            if (EqualityComparer<T>.Default.Equals(_startNode, node))
                return;

            //old node: event is called with false
            if (_startNode != null)
                StartNodeSet?.Invoke(_startNode, false);

            //new node: event is called with true
            _startNode = node;
            StartNodeSet?.Invoke(_startNode, true);
        }

        public void SetFinishNode(T node)
        {
            if (EqualityComparer<T>.Default.Equals(_finishNode, node))
                return;

            //old node: event is called with false
            if (_finishNode != null)
                FinishNodeSet?.Invoke(_finishNode, false);

            //new node: event is called with true
            _finishNode = node;
            FinishNodeSet?.Invoke(_finishNode, true);
        }

        public void CheckStartAndFinishReady()
        {
            StartAndFinishReady?.Invoke(_startNode != null && _finishNode != null);
        }

        public IList<T> GetPath()
        {
            return _searchAlgorithm.CalculateWay(_startNode, _finishNode, _heuristicFunction);
        }
    }
}
