using System.Collections.Generic;
using System.Security.Cryptography;
using ThisProject.GridNeighbours;
using ThisProject.Links.Factories;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Links.Providers
{
    /// <summary>
    /// Creates links during search algorithm work - not beforehand
    /// </summary>
    public class GridDynamicLinksProvider<TNodeData, TLinkData> : ILinksProvider<TLinkData, Vector2Int>
        where TNodeData : INodeData<Vector2Int>
        where TLinkData : ILinkData<Vector2Int>
    {
        private readonly ILinkDataFactory<TNodeData, TLinkData, Vector2Int> _factory;
        private readonly IGridNeighboursProvider<TNodeData> _neighboursProvider;
        private readonly GridTypeStorage<TNodeData> _nodeDatas;


        public GridDynamicLinksProvider(ILinkDataFactory<TNodeData, TLinkData, Vector2Int> factory, IGridNeighboursProvider<TNodeData> neighboursProvider, GridTypeStorage<TNodeData> nodeDatas)
        {
            _factory = factory;
            _neighboursProvider = neighboursProvider;
            _nodeDatas = nodeDatas;
        }

        public IEnumerable<TLinkData> GetLinksFromNode(Vector2Int id)
        {
            var neighbours = _nodeDatas.GetNeighbourObjects(id, _neighboursProvider);
            var node = _nodeDatas.GetItem(id);

            return _factory.CreateLinksFromNode(node, neighbours);
        }

        public IEnumerable<TLinkData> GetLinksToNode(Vector2Int id)
        {
            var neighbours = _nodeDatas.GetNeighbourObjects(id, _neighboursProvider);
            var node = _nodeDatas.GetItem(id);

            return _factory.CreateLinksToNode(node, neighbours);
        }
    }
}