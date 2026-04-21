using System;
using System.Collections.Generic;
using Core.HeuristicFunctions;
using Core.Nodes;
using Core.Nodes.Cells;
using Core.SearchAlgorithms;

namespace Core.PathFinders
{
    public class PathFinder : IPathFinder<Cell>
    {
        private IHeuristicFunction _heuristicFunction;
        private ISearchAlgorithm<Cell> _searchAlgorithm;

        private Cell _startNode;
        private Cell _finishNode;
        
        public event Action<Cell, bool> StartNodeSet;
        public event Action<Cell, bool> FinishNodeSet;
        public event Action StartFinishReady;


        public PathFinder(IHeuristicFunction heuristicFunction, ISearchAlgorithm<Cell> searchAlgorithm)
        {
            _heuristicFunction = heuristicFunction;
            _searchAlgorithm = searchAlgorithm;
        }

        public void SetStartNode(Cell newNode)
        {
            if (_startNode == newNode) 
                return;

            var oldNode = _startNode;
            _startNode = newNode;

            StartNodeSet?.Invoke(oldNode, false);
            StartNodeSet?.Invoke(newNode, true);
        }

        public void SetFinishNode(Cell newNode)
        {
            if (_finishNode == newNode)
                return;

            var oldNode = _finishNode;
            _finishNode = newNode;

            FinishNodeSet?.Invoke(oldNode, false);
            FinishNodeSet?.Invoke(newNode, true);
        }

        protected void CheckStartFinishReady() //
        {
            if (_startNode != null && _finishNode != null)
                StartFinishReady?.Invoke();
        }

        public IList<Cell> GetPath()
        {
            return _searchAlgorithm.CalculateWay(_startNode, _finishNode, _heuristicFunction);
        }
    }
}
