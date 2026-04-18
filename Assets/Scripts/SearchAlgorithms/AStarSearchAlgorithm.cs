using Nodes;
using System.Collections.Generic;
using Fields;

namespace Core.SearchAlgorithms
{
    public class AStarSearchAlgorithm : ISearchAlgorithm
    {
        private Dictionary<INode, INode> _cameFrom;
        private Dictionary<INode, float> _costSoFar;


        public IList<INode> GetPath(AbstractField field)
        {
            return CalculateWay(field);
        }

        private IList<INode> CalculateWay(AbstractField field)
        {
            _cameFrom = new Dictionary<INode, INode>();
            _costSoFar = new Dictionary<INode, float>();

            var needToCheck = new PriorityQueue<INode>();
            needToCheck.Enqueue(field.StartNode, 0);

            _cameFrom[field.StartNode] = field.StartNode;
            _costSoFar[field.StartNode] = 0;

            while (needToCheck.Count > 0)
            {
                var current = needToCheck.Dequeue();

                if (current == field.FinishNode)
                {
                    break;
                }

                foreach (var link in current.Links)
                {
                    var newCost = _costSoFar[current] + link.Cost;
                    if (!_costSoFar.ContainsKey(link.To) || newCost < _costSoFar[link.To])
                    {
                        _costSoFar[link.To] = newCost;
                        var priority = newCost + field.EstimateCost(link.To, field.FinishNode);
                        needToCheck.Enqueue(link.To, priority);
                        _cameFrom[link.To] = current;
                    }
                }
            }

            var node = field.FinishNode;

            if (!_cameFrom.ContainsKey(node))
            {
                return null;
            }

            var path = new List<INode>();
            path.Add(node);

            while (true)
            {
                node = _cameFrom[node];
                path.Add(node);

                if (node == field.StartNode)
                    break;
            }

            return path;
        }
    }
}
