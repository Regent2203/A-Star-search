using ThisProject.Links.Factories.CostProviders;
using ThisProject.Nodes;
using System.Collections.Generic;
using ThisProject.ObjectsStorages;

namespace ThisProject.Links.Factories
{
    public class LinksFactory<T, TId> : ILinksFactory<T, LinkData<TId>, TId>
        where T : INodeData<TId>
    {
        private readonly IObjectsStorage<T, TId> _nodeDatas;
        private readonly ICostProvider<T> _costProvider;


        public LinksFactory(IObjectsStorage<T, TId> nodeDatas, ICostProvider<T> costProvider)
        {
            _nodeDatas = nodeDatas;
            _costProvider = costProvider;
        }

        public IEnumerable<LinkData<TId>> CreateLinksFromNode(T from, IEnumerable<T> neighbours)
        {
            foreach (var to in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public IEnumerable<LinkData<TId>> CreateLinksToNode(T to, IEnumerable<T> neighbours)
        {
            foreach (var from in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public LinkData<TId> CreateLink(T from, T to)
        {            
            return CreateLinkInternal(from, to);
        }

        private LinkData<TId> CreateLinkInternal(T from, T to)
        {
            var cost = _costProvider.GetCost(from, to);

            //todo some factory
            var linkData = new LinkData<TId>();
            linkData.OnSpawned(from.Id, to.Id, cost);

            return linkData;
        }
    }
}