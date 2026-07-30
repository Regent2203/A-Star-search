using System.Collections.Generic;
using ThisProject.Heuristic;
using ThisProject.Links.Providers;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;

namespace ThisProject.SearchAlgorithms
{
    public class AStarSearchAlgorithm<T,TId> : ISearchAlgorithm<T, TId>
        where T : INodeData<TId>
    {
        private Dictionary<T, T> _cameFrom;
        private Dictionary<T, float> _costSoFar;

        private readonly IObjectsStorage<T, TId> _nodes;


        public AStarSearchAlgorithm(IObjectsStorage<T, TId> nodes) 
        {
            _nodes = nodes;
        }

        public IList<T> CalculateWay(T startNode, T finishNode, IHeuristicsProvider<T> heuristicsProvider, ILinksProvider<T, TId> linksProvider)
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

                foreach (var link in linksProvider.GetLinksFromNode(current))
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

                        var newPriority = newCost + heuristicsProvider.EstimateCost(toNode, finishNode);
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