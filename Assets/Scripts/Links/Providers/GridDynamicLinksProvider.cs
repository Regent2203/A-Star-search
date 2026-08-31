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
    public class GridDynamicLinksProvider<TNodeData, TLinkData> : ILinksProvider<TLinkData, Vector2Int>
        where TNodeData : INodeData<Vector2Int>
        where TLinkData : ILinkData<Vector2Int>
    {
        private readonly List<TLinkData> _links = new(8);

        private readonly SmartLinkDataFactory<TNodeData, TLinkData, Vector2Int> _smartFactory;
        private readonly IGridNeighboursProvider<TNodeData> _neighboursProvider;
        private readonly GridTypeStorage<TNodeData> _nodeDatas;


        public GridDynamicLinksProvider(SmartLinkDataFactory<TNodeData, TLinkData, Vector2Int> factory,
            IGridNeighboursProvider<TNodeData> neighboursProvider, GridTypeStorage<TNodeData> nodeDatas)
        {
            _smartFactory = factory;
            _neighboursProvider = neighboursProvider;
            _nodeDatas = nodeDatas;
        }

        public IEnumerable<TLinkData> GetLinksFromNode(Vector2Int id)
        {
            ClearDynamicLinks();

            var neighbours = _nodeDatas.GetNeighbourObjects(id, _neighboursProvider);
            var node = _nodeDatas.GetItem(id);
            
            var links = _smartFactory.CreateLinksFromNode(node, neighbours);
            _links.AddRange(links);

            return _links;
        }

        public IEnumerable<TLinkData> GetLinksToNode(Vector2Int id)
        {
            ClearDynamicLinks();

            var neighbours = _nodeDatas.GetNeighbourObjects(id, _neighboursProvider);
            var node = _nodeDatas.GetItem(id);
            
            var links = _smartFactory.CreateLinksToNode(node, neighbours);
            _links.AddRange(links);

            return _links;
        }

        private void ClearDynamicLinks()
        {
            foreach (var link in _links)
            {
                _smartFactory.DeleteItem(link);
            }
            _links.Clear();
        }
    }
}