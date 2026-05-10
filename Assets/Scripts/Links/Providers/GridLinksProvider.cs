using Core.Fields.Grids.Neighbours;
using Core.Links;
using Core.Links.Factories;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Links.Providers
{
    public class GridLinksProvider<T> : ILinksProvider<T, Vector2Int> where T : class, INode<Vector2Int>
    {
        private T[,] _gridNodes;

        private readonly GridLinksFactory<T> _factory;
        private readonly IGridNeighboursProvider<T> _neighboursProvider;


        public GridLinksProvider(GridLinksFactory<T> factory, IGridNeighboursProvider<T> neighboursProvider)
        {
            _factory = factory;
            _neighboursProvider = neighboursProvider;
        }

        public void InitGrid(T[,] gridNodes)
        {
            _gridNodes = gridNodes;
        }

        public IEnumerable<ILink<T, Vector2Int>> GetLinksForNode(T node)
        {
            var neighbours = _neighboursProvider.GetNeighbours(node.Id, _gridNodes);

            return _factory.CreateNeighbourLinksForNode(node, neighbours);
        }
    }
}