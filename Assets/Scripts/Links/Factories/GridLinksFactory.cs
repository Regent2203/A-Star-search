using Core.CostProviders;
using Core.Links;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Links.Factories
{
    /// <summary>
    /// Used to create links for cells in grid during search algorithm work (not beforehand)
    /// </summary>
    public class GridLinksFactory<T> : LinksFactory<T, Vector2Int> where T : class, INode<T, Vector2Int>
    {
        private readonly ICostProvider<T, Vector2Int> _costProvider;


        public GridLinksFactory(ICostProvider<T, Vector2Int> costProvider)
        {
            _costProvider = costProvider;
        }
        
        //todo: rework
        public IEnumerable<ILink<T, Vector2Int>> CreateNeighbourLinksForNode(T from, IEnumerable<T> neighbours)
        {
            if (from.IsBlocked)
                yield break;

            foreach (var to in neighbours)
            {
                if (to.IsBlocked)
                    continue;

                var cost = _costProvider.GetCost(from, to);

                yield return CreateLink(from, to, cost);
            }
        }
    }
}