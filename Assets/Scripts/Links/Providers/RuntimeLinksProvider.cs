using Core.Fields.Grids.Neighbours;
using Core.Links.Factories;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Links.Providers
{
    /// <summary>
    /// Creates links during search algorithm work - not beforehand
    /// </summary>
    public class RuntimeLinksProvider<T> : ILinksProvider<T> where T : class, INode<Vector2Int>
    {
        private T[,] _gridNodes;

        private readonly ILinksFactory<T> _factory;
        private readonly IGridNeighboursProvider<T> _neighboursProvider;
        
        //todo: add cache dictionary


        public RuntimeLinksProvider(ILinksFactory<T> factory, IGridNeighboursProvider<T> neighboursProvider)
        {
            _factory = factory;
            _neighboursProvider = neighboursProvider;
        }

        public void InitGrid(T[,] gridNodes)
        {
            _gridNodes = gridNodes;
        }

        public IEnumerable<ILink<T>> GetLinksForNode(T node)
        {
            var neighbours = _neighboursProvider.GetNeighbours(node.Id, _gridNodes);

            return _factory.CreateLinks(node, neighbours);
        }
    }
}