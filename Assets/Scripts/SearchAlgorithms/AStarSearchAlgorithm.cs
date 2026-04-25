using Core.HeuristicFunctions;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.SearchAlgorithms
{
    public class AStarSearchAlgorithm<T> : ISearchAlgorithm<T> where T : INode<T>
    {
        private Dictionary<T, T> _cameFrom;
        private Dictionary<T, float> _costSoFar;


        public IList<T> CalculateWay(T startNode, T finishNode, IHeuristicFunction heuristicFunction)
        {
            if (EqualityComparer<T>.Default.Equals(startNode, finishNode))
                return null;

            _cameFrom = new Dictionary<T, T>();
            _costSoFar = new Dictionary<T, float>();

            var needToCheck = new PriorityQueue<T>();
            needToCheck.Enqueue(startNode, 0);

            _cameFrom[startNode] = startNode;
            _costSoFar[startNode] = 0;

            while (needToCheck.Count > 0)
            {
                var current = needToCheck.Dequeue();

                if (EqualityComparer<T>.Default.Equals(current, finishNode))
                {
                    break;
                }

                foreach (var link in current.Links)
                {
                    var newCost = _costSoFar[current] + link.Cost;
                    if (!_costSoFar.ContainsKey(link.To) || newCost < _costSoFar[link.To])
                    {
                        _costSoFar[link.To] = newCost;
                        var priority = newCost + heuristicFunction.EstimateCost(link.To, finishNode);
                        needToCheck.Enqueue(link.To, priority);
                        _cameFrom[link.To] = current;
                    }
                }
            }

            var node = finishNode;

            if (!_cameFrom.ContainsKey(node))
            {
                return null;
            }

            var path = new List<T>();
            path.Add(node);

            while (true)
            {
                node = _cameFrom[node];
                path.Add(node);

                if (EqualityComparer<T>.Default.Equals(node, startNode))
                    break;
            }

            return path;
        }
    }
}
