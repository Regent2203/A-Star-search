using ThisProject.Links.Factories.CostProviders;
using ThisProject.Nodes;
using System.Collections.Generic;

namespace ThisProject.Links.Factories
{
    public class LinksFactory<T, TId> : ILinksFactory<T, TId>
        where T : INodeData<TId>
    {
        private readonly ICostProvider<T> _costProvider;


        public LinksFactory(ICostProvider<T> costProvider)
        {
            _costProvider = costProvider;
        }

        public IEnumerable<ILinkData<TId>> CreateLinksFromNode(T from, IEnumerable<T> neighbours)
        {
            foreach (var to in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public IEnumerable<ILinkData<TId>> CreateLinksToNode(T to, IEnumerable<T> neighbours)
        {
            foreach (var from in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public ILinkData<TId> CreateLink(T from, T to)
        {            
            return CreateLinkInternal(from, to);
        }

        private ILinkData<TId> CreateLinkInternal(T from, T to)
        {
            var cost = _costProvider.GetCost(from, to);

            return new LinkData<TId>(from.Id, to.Id, cost);
        }
    }
}