using Core.CostProviders;
using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links.Factories
{
    public class LinksFactory<T> : ILinksFactory<T> where T : class, INode
    {
        private readonly ICostProvider<T> _costProvider;


        public LinksFactory(ICostProvider<T> costProvider)
        {
            _costProvider = costProvider;
        }

        public IEnumerable<ILink<T>> CreateLinks(T from, IEnumerable<T> neighbours)
        {
            foreach (var to in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public ILink<T> CreateLink(T from, T to)
        {
            var cost = _costProvider.GetCost(from, to);
            return CreateLinkInternal(from, to, cost);
        }

        private ILink<T> CreateLinkInternal(T from, T to, float cost)
        {
            return new Link<T>(from, to, cost);
        }
    }
}