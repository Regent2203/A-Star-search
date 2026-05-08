using Core.CostProviders;
using Core.Links;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields.Grids
{
    /// <summary>
    /// Used to create links in for cells in grid during search algorithm work (not beforehand)
    /// </summary>
    public class LinksProvider<T, TId> : ILinksProvider<T, TId> where T : class, INode<T, TId>
    {
        private readonly ICostProvider<T, TId> _costProvider;

        
        public LinksProvider(ICostProvider<T, TId> costProvider)
        {
            _costProvider = costProvider;
        }

        public IEnumerable<ILink<T, TId>> GetLinks(T from, IEnumerable<T> neighbours)
        {
            if (from.IsBlocked)
                yield break;

            foreach (var to in neighbours)
            {
                if (to.IsBlocked)
                    continue;

                var cost = _costProvider.GetCost(from, to);

                yield return new Link<T, TId>(from, to, cost);
            }
        }
    }
}