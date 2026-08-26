using EasyField.Heuristic;
using EasyField.Links;
using EasyField.Links.Providers;
using EasyField.Nodes;
using EasyField.ObjectsStorages;
using System.Collections.Generic;
using UnityEngine;

namespace EasyField.SearchAlgorithms
{
    public class AStarSearchAlgorithm<TNodeData, TLinkData, TId> : ISearchAlgorithm<TNodeData>
        where TNodeData : INodeData<TId>
        where TLinkData : ILinkData<TId>
    {
        private readonly List<TNodeData> _resultPath = new();
        private readonly Dictionary<TNodeData, TNodeData> _cameFrom = new(); //[nextNode, prevNode] - came to nextNode from prevNode, shortest known path
        private readonly Dictionary<TNodeData, float> _costSoFar = new(); //[keyNode, cost] - contains minimum known cost for path from startNode to keyNode 

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

            Debug.Log("==== Algorithm start ====");
            Debug.Log($"Path from {startNode.Id} to {finishNode.Id}");

            _cameFrom.Clear();
            _costSoFar.Clear();

            var needToCheck = new PriorityQueue<TNodeData>();
            needToCheck.Enqueue(startNode, 0);

            _cameFrom[startNode] = default;
            _costSoFar[startNode] = 0;

            while (needToCheck.Count > 0)
            {
                var fromNode = needToCheck.Dequeue();
                Debug.Log($"<color=#0000FF>Checking node:</color> {fromNode.Id} (nodes to check: {needToCheck.Count})");

                if (fromNode.Equals(finishNode))
                {
                    Debug.Log("Finish node reached!");
                    Debug.Log("==== Algorithm finish ====");
                    return RetracePath(startNode, finishNode);
                }

                Debug.Log($"Cost from start node to this node: {_costSoFar[fromNode]}");
                foreach (var link in _linksProvider.GetLinksFromNode(fromNode.Id))
                {
                    var toNode = _nodes.GetItem(link.To);

                    if (fromNode.IsBlocked || toNode.IsBlocked)
                        continue;
                    
                    Debug.Log($"<color=#00FFFF>Checking link</color> from node {fromNode.Id} to node {toNode.Id}: cost = {link.Cost}");
                    var newCost = _costSoFar[fromNode] + link.Cost;

                    if (!_costSoFar.ContainsKey(toNode) || newCost < _costSoFar[toNode]) //we found shorter path
                    {
                        Debug.Log($"New cost is better: {newCost} (was {(_costSoFar.TryGetValue(toNode, out var value) ? value : "null")})");
                        _costSoFar[toNode] = newCost;
                        _cameFrom[toNode] = fromNode;

                        var newPriority = newCost + _heuristicsProvider.EstimateCost(toNode, finishNode);
                        Debug.Log($"Estimation from node {toNode.Id} to finish node: {newPriority - newCost}, priority: {newPriority}");
                        needToCheck.Enqueue(toNode, newPriority);
                    }
                }
            }

            return null;
        }

        private List<TNodeData> RetracePath(TNodeData startNode, TNodeData finishNode)
        {
            _resultPath.Clear();

            var current = finishNode;

            while (!current.Equals(startNode))
            {
                _resultPath.Add(current);
                current = _cameFrom[current];
            }

            _resultPath.Add(startNode);
            _resultPath.Reverse();

            return _resultPath;
        }
    }
}