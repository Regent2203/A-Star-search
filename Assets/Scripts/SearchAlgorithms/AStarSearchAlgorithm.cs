using Core.Heuristic;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.SearchAlgorithms
{
    public class AStarSearchAlgorithm : ISearchAlgorithm
    {
        private Dictionary<INode, INode> _cameFrom;
        private Dictionary<INode, float> _costSoFar;


        public IList<INode> CalculateWay(INode startNode, INode finishNode, IHeuristicsProvider heuristicsController)
        {
            if (startNode.Equals(finishNode))
                return null;

            _cameFrom = new Dictionary<INode, INode>();
            _costSoFar = new Dictionary<INode, float>();

            var needToCheck = new PriorityQueue<INode>();
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

        private IList<INode> RetracePath(INode startNode, INode finishNode)
        {
            var path = new List<INode>();
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