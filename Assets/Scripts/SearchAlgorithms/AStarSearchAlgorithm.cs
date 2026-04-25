using Core.Heuristic;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.SearchAlgorithms
{
    public class AStarSearchAlgorithm<T> : ISearchAlgorithm<T> where T : INode<T>
    {
        private Dictionary<T, T> _cameFrom;
        private Dictionary<T, float> _costSoFar;


        public IList<T> CalculateWay(T startNode, T finishNode, IHeuristicsProvider heuristicsController)
        {
            if (startNode.Equals(finishNode))
                return null;

            _cameFrom = new Dictionary<T, T>();
            _costSoFar = new Dictionary<T, float>();

            var needToCheck = new PriorityQueue<T>();
            needToCheck.Enqueue(startNode, 0);

            _cameFrom[startNode] = default;
            _costSoFar[startNode] = 0;

            while (needToCheck.Count > 0)
            {
                var current = needToCheck.Dequeue();

                if (current.Equals(finishNode))
                {
                    return RetracePath(startNode, finishNode);
                }

                foreach (var link in current.Links)
                {
                    var newCost = _costSoFar[current] + link.Cost;

                    if (!_costSoFar.ContainsKey(link.To) || newCost < _costSoFar[link.To])
                    {
                        _costSoFar[link.To] = newCost;
                        _cameFrom[link.To] = current;

                        var newPriority = newCost + heuristicsController.EstimateCost(link.To, finishNode);
                        needToCheck.Enqueue(link.To, newPriority);
                    }
                }
            }

            return null;
        }

        private IList<T> RetracePath(T startNode, T finishNode)
        {
            var path = new List<T>();
            var current = finishNode;

            while (!current.Equals(startNode))
            {
                path.Add(current);
                current = _cameFrom[current];
            }

            path.Add(startNode);
            path.Reverse();
            return path;
        }
    }
}
