using EasyField.GridNeighbours;
using EasyField.Links.Factories;
using EasyField.Nodes;
using EasyField.ObjectsStorages;
using System.Collections.Generic;
using UnityEngine;

namespace EasyField.Links.Providers
{
    /// <summary>
    /// Creates links during search algorithm work - not beforehand
    /// </summary>
    public class GridDynamicLinksProvider<TNodeData> : ILinksProvider<ILinkData<Vector2Int>, Vector2Int>
        where TNodeData : INodeData<Vector2Int>
    {
        private readonly SmartLinkDataFactory<TNodeData, Vector2Int> _factory;
        private readonly IGridNeighboursProvider<TNodeData> _neighboursProvider;
        private readonly GridTypeStorage<TNodeData> _nodeDatas;


        public GridDynamicLinksProvider(SmartLinkDataFactory<TNodeData, Vector2Int> factory, IGridNeighboursProvider<TNodeData> neighboursProvider, GridTypeStorage<TNodeData> nodeDatas)
        {
            _factory = factory;
            _neighboursProvider = neighboursProvider;
            _nodeDatas = nodeDatas;
        }

        public IEnumerable<ILinkData<Vector2Int>> GetLinksFromNode(Vector2Int id)
        {
            var neighbours = _nodeDatas.GetNeighbourObjects(id, _neighboursProvider);
            var node = _nodeDatas.GetItem(id);

            return _factory.CreateLinksFromNode(node, neighbours);
        }

        public IEnumerable<ILinkData<Vector2Int>> GetLinksToNode(Vector2Int id)
        {
            var neighbours = _nodeDatas.GetNeighbourObjects(id, _neighboursProvider);
            var node = _nodeDatas.GetItem(id);

            return _factory.CreateLinksToNode(node, neighbours);
        }
    }
}