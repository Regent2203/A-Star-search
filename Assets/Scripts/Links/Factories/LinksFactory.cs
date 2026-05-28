using ThisProject.Links.Factories.CostProviders;
using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.Links.Factories
{
    public class LinksFactory<T> : ILinksFactory<T> where T : class, INode
    {
        private readonly ICostProvider<T> _costProvider;


        public LinksFactory(ICostProvider<T> costProvider)
        {
            _costProvider = costProvider;
        }

        public IEnumerable<ILink<T>> CreateLinksFromNode(T from, IEnumerable<T> neighbours)
        {
            foreach (var to in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public IEnumerable<ILink<T>> CreateLinksToNode(T to, IEnumerable<T> neighbours)
        {
            foreach (var from in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public ILink<T> CreateLink(T from, T to)
        {            
            return CreateLinkInternal(from, to);
        }

        private ILink<T> CreateLinkInternal(T from, T to)
        {
            var cost = _costProvider.GetCost(from, to);

            return new Link<T>(from, to, cost);
        }
    }
}