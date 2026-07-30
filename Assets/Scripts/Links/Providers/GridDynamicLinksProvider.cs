using System.Collections.Generic;
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
    public class GridDynamicLinksProvider<T> : ILinksProvider<T, Vector2Int>
        where T : INodeData<Vector2Int>
    {
        private readonly ILinksFactory<T, Vector2Int> _factory;
        private readonly IGridNeighboursProvider<T> _neighboursProvider;
        private readonly GridTypeStorage<T> _gridNodes;


        public GridDynamicLinksProvider(ILinksFactory<T, Vector2Int> factory, IGridNeighboursProvider<T> neighboursProvider, GridTypeStorage<T> gridNodes)
        {
            _factory = factory;
            _neighboursProvider = neighboursProvider;
            _gridNodes = gridNodes;
        }

        public IEnumerable<ILinkData<Vector2Int>> GetLinksFromNode(T node)
        {
            var neighbours = _gridNodes.GetNeighbourObjects(node.Id, _neighboursProvider);

            return _factory.CreateLinksFromNode(node, neighbours);
        }

        public IEnumerable<ILinkData<Vector2Int>> GetLinksToNode(T node)
        {
            var neighbours = _gridNodes.GetNeighbourObjects(node.Id, _neighboursProvider);

            return _factory.CreateLinksToNode(node, neighbours);
        }
    }
}