using Core.Fields;
using Core.Heuristic;
using Core.Links;
using Core.Links.Providers;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.SearchAlgorithms
{
    public class AStarSearchAlgorithm<T, TId> : ISearchAlgorithm<T, TId> where T : class, INode<TId>
    {
        private Dictionary<T, T> _cameFrom;
        private Dictionary<T, float> _costSoFar;

        private readonly ILinksProvider<T, TId> _linksProvider;


        public AStarSearchAlgorithm(ILinksProvider<T, TId> linksProvider)
        {
            _linksProvider = linksProvider;
        }

        public IList<T> CalculateWay(T startNode, T finishNode, IHeuristicsProvider<T, TId> heuristicsController)
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

                foreach (var link in _linksProvider.GetLinksForNode(current))
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