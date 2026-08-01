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
    public class GridDynamicLinksProvider<T, L> : ILinksProvider<T, L, Vector2Int>
        where T : INodeData<Vector2Int>
        where L : ILinkData<Vector2Int>
    {
        private readonly ILinksFactory<T, L, Vector2Int> _factory;
        private readonly IGridNeighboursProvider<T> _neighboursProvider;
        private readonly GridTypeStorage<T> _gridNodes;


        public GridDynamicLinksProvider(ILinksFactory<T, L, Vector2Int> factory, IGridNeighboursProvider<T> neighboursProvider, GridTypeStorage<T> gridNodes)
        {
            _factory = factory;
            _neighboursProvider = neighboursProvider;
            _gridNodes = gridNodes;
        }

        public IEnumerable<L> GetLinksFromNode(T node)
        {
            var neighbours = _gridNodes.GetNeighbourObjects(node.Id, _neighboursProvider);

            return _factory.CreateLinksFromNode(node, neighbours);
        }

        public IEnumerable<L> GetLinksToNode(T node)
        {
            var neighbours = _gridNodes.GetNeighbourObjects(node.Id, _neighboursProvider);

            return _factory.CreateLinksToNode(node, neighbours);
        }
    }
}