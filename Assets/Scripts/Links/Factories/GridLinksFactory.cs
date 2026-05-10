using Core.CostProviders;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Links.Factories
{
    /// <summary>
    /// Used to create links for cells in grid during search algorithm work (not beforehand)
    /// </summary>
    public class GridLinksFactory<T> : LinksFactory<T> where T : class, INode
    {
        private readonly ICostProvider<T> _costProvider;


        public GridLinksFactory(ICostProvider<T> costProvider)
        {
            _costProvider = costProvider;
        }
        
        //todo: rework
        public IEnumerable<ILink<T>> CreateNeighbourLinksForNode(T from, IEnumerable<T> neighbours)
        {
            if (from.IsBlocked) //todo: move IsBlocked to search algorithm (skipping)
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