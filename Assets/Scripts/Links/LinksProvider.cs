using Core.CostProviders;
using Core.Links;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields.Grids
{
    /// <summary>
    /// Used to create links in for cells in grid during search algorithm work (not beforehand)
    /// </summary>
    public class LinksProvider<T> : ILinksProvider<T> where T: class, INode<T>
    {
        private readonly ICostProvider<T> _costProvider;

        
        public LinksProvider(ICostProvider<T> costProvider)
        {
            _costProvider = costProvider;
        }

        public IEnumerable<ILink<T>> GetLinks(T from, IEnumerable<T> neighbours)
        {
            if (from.IsBlocked)
                yield break;

            foreach (var to in neighbours)
            {
                if (to.IsBlocked)
                    continue;

                var cost = _costProvider.GetCost(from, to);

                yield return new Link<T>(from, to, cost);
            }
        }
    }
}