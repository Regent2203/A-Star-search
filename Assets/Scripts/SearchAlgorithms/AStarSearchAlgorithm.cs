using System.Collections.Generic;
using ThisProject.Heuristic;
using ThisProject.Links;
using ThisProject.Links.Providers;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;

namespace ThisProject.SearchAlgorithms
{
    public class AStarSearchAlgorithm<T, L, TId> : ISearchAlgorithm<T>
        where T : INodeData<TId>
        where L : ILinkData<TId>
    {
        private Dictionary<T, T> _cameFrom;
        private Dictionary<T, float> _costSoFar;

        private readonly IObjectsStorage<T, TId> _nodes;
        private readonly IHeuristicsProvider<T> _heuristicsProvider;
        private readonly ILinksProvider<T, L, TId> _linksProvider;


        public AStarSearchAlgorithm(IObjectsStorage<T, TId> nodes, IHeuristicsProvider<T> heuristicsProvider, ILinksProvider<T, L, TId> linksProvider) 
        {
            _nodes = nodes;
            _heuristicsProvider = heuristicsProvider;
            _linksProvider = linksProvider;
        }

        public IList<T> CalculateWay(T startNode, T finishNode)
        {
            if (startNode.Equals(finishNode))
                return null;

            T fromNode;
            T toNode;

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

                foreach (var link in _linksProvider.GetLinksFromNode(current))
                {
                    fromNode = _nodes.GetItem(link.From);
                    toNode = _nodes.GetItem(link.To);

                    if (fromNode.IsBlocked || toNode.IsBlocked)
                        continue;

                    var newCost = _costSoFar[current] + link.Cost;

                    if (!_costSoFar.ContainsKey(toNode) || newCost < _costSoFar[toNode])
                    {
                        _costSoFar[toNode] = newCost;
                        _cameFrom[toNode] = current;

                        var newPriority = newCost + _heuristicsProvider.EstimateCost(toNode, finishNode);
                        needToCheck.Enqueue(toNode, newPriority);
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