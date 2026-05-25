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
    public class RuntimeLinksProvider<T> : ILinksProvider<T> where T : class, INode<Vector2Int>
    {
        private readonly ILinksFactory<T> _factory;
        private readonly IGridNeighboursProvider<T> _neighboursProvider;
        private readonly GridTypeStorage<T> _gridNodes;


        public RuntimeLinksProvider(ILinksFactory<T> factory, IGridNeighboursProvider<T> neighboursProvider, GridTypeStorage<T> gridNodes)
        {
            _factory = factory;
            _neighboursProvider = neighboursProvider;
            _gridNodes = gridNodes;
        }

        public IEnumerable<ILink<T>> GetLinksForNode(T node)
        {
            var neighbours = _gridNodes.GetNeighbourObjects(node.Id, _neighboursProvider);

            return _factory.CreateLinks(node, neighbours);
        }
    }
}