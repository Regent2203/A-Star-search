using Core.CostProviders;
using Core.Links;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.Fields.Grids
{
    public class LinksProvider : ILinksProvider
    {
        private readonly ICostProvider _costProvider;

        
        public LinksProvider(ICostProvider costProvider)
        {
            _costProvider = costProvider;
        }

        public IEnumerable<ILink> GetLinks(INode from, IEnumerable<INode> neighbours)
        {
            if (from.IsBlocked)
                yield break;

            foreach (var to in neighbours)
            {
                if (to.IsBlocked)
                    continue;

                var cost = _costProvider.GetCost(from, to);

                yield return new Link(from, to, cost);
            }
        }
    }
}