using ThisProject.Fields.GridNeighbours;
using ThisProject.Links.Factories;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using System.Collections.Generic;
using UnityEngine;

namespace ThisProject.Links.Providers
{
    /// <summary>
    /// Creates links during search algorithm work - not beforehand
    /// </summary>
    public class GridDynamicLinksProvider<T> : ILinksProvider<T>
        where T : INodeData<Vector2Int>
    {
        private readonly ILinksFactory<T> _factory;
        private readonly IGridNeighboursProvider<T> _neighboursProvider;
        private readonly GridTypeStorage<T> _gridNodes;


        public GridDynamicLinksProvider(ILinksFactory<T> factory, IGridNeighboursProvider<T> neighboursProvider, GridTypeStorage<T> gridNodes)
        {
            _factory = factory;
            _neighboursProvider = neighboursProvider;
            _gridNodes = gridNodes;
        }

        public IEnumerable<ILinkData<T>> GetLinksFromNode(T node)
        {
            var neighbours = _gridNodes.GetNeighbourObjects(node.Id, _neighboursProvider);

            return _factory.CreateLinksFromNode(node, neighbours);
        }

        public IEnumerable<ILinkData<T>> GetLinksToNode(T node)
        {
            var neighbours = _gridNodes.GetNeighbourObjects(node.Id, _neighboursProvider);

            return _factory.CreateLinksToNode(node, neighbours);
        }
    }
}