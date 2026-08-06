using System.Collections.Generic;
using EasyField.Heuristic;
using EasyField.Links;
using EasyField.Links.Providers;
using EasyField.Nodes;
using EasyField.ObjectsStorages;
using UnityEngine;

namespace EasyField.SearchAlgorithms
{
    public class AStarSearchAlgorithm<TNodeData, TLinkData, TId> : ISearchAlgorithm<TNodeData>
        where TNodeData : INodeData<TId>
        where TLinkData : ILinkData<TId>
    {
        private Dictionary<TNodeData, TNodeData> _cameFrom;
        private Dictionary<TNodeData, float> _costSoFar;

        private readonly IObjectsStorage<TNodeData, TId> _nodes;
        private readonly IHeuristicsProvider<TNodeData> _heuristicsProvider;
        private readonly ILinksProvider<TLinkData, TId> _linksProvider;


        public AStarSearchAlgorithm(IObjectsStorage<TNodeData, TId> nodes, IHeuristicsProvider<TNodeData> heuristicsProvider, ILinksProvider<TLinkData, TId> linksProvider)
        {
            _nodes = nodes;
            _heuristicsProvider = heuristicsProvider;
            _linksProvider = linksProvider;
        }

        public IList<TNodeData> CalculateWay(TNodeData startNode, TNodeData finishNode)
        {
            if (startNode.Equals(finishNode))
                return null;

            TNodeData fromNode;
            TNodeData toNode;

            _cameFrom = new Dictionary<TNodeData, TNodeData>();
            _costSoFar = new Dictionary<TNodeData, float>();

            var needToCheck = new PriorityQueue<TNodeData>();
            needToCheck.Enqueue(startNode, 0);

            _cameFrom[startNode] = default;
            _costSoFar[startNode] = 0;

            while (needToCheck.Count > 0)
            {
                var current = needToCheck.Dequeue();

                if (current.Equals(finishNode))
                {
                    Debug.Log($"{_costSoFar[finishNode]}");
                    return RetracePath(startNode, finishNode);
                }

                foreach (var link in _linksProvider.GetLinksFromNode(current.Id))
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

        private IList<TNodeData> RetracePath(TNodeData startNode, TNodeData finishNode)
        {
            var path = new List<TNodeData>();
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