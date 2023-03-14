using Nodes;
using System;
using System.Collections.Generic;
using UnityEngine;
using Fields;

namespace Algorithm
{
    public class AStarSearchAlgorithm
    {
        private AbstractField _field;
        private Dictionary<INode, INode> _cameFrom;
        private Dictionary<INode, float> _costSoFar;


        public AStarSearchAlgorithm(AbstractField field)
        {
            _field = field;
        }

        public IList<INode> CalculateWay()
        {
            //Debug.Log("Path calculating started...");

            _cameFrom = new Dictionary<INode, INode>();
            _costSoFar = new Dictionary<INode, float>();

            var needToCheck = new PriorityQueue<INode>();
            needToCheck.Enqueue(_field.StartNode, 0);

            _cameFrom[_field.StartNode] = _field.StartNode;
            _costSoFar[_field.StartNode] = 0;

            while (needToCheck.Count > 0)
            {
                var current = needToCheck.Dequeue();

                if (current == _field.FinishNode)
                {
                    break;
                }

                foreach (var link in current.Links)
                {
                    var newCost = _costSoFar[current] + link.Cost;
                    if (!_costSoFar.ContainsKey(link.To) || newCost < _costSoFar[link.To])
                    {
                        _costSoFar[link.To] = newCost;
                        var priority = newCost + _field.EstimateCost(link.To, _field.FinishNode);
                        needToCheck.Enqueue(link.To, priority);
                        _cameFrom[link.To] = current;
                    }

                }
            }
            //Debug.Log("Path calculating finished...");

            return GetPath();
        }

        private IList<INode> GetPath()
        {
            var node = _field.FinishNode;

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
                
                if (node == _field.StartNode)
                    break;
            }

            return path;
        }
    }
}
