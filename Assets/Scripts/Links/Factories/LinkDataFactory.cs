using System.Collections.Generic;
using EasyField.Links.CostProviders;
using EasyField.Links.Implementations;
using EasyField.Nodes;

namespace EasyField.Links.Factories
{
    public class LinkDataFactory<TNodeData, TId> : ILinkDataFactory<TNodeData, LinkData<TId>, TId>
        where TNodeData : INodeData<TId>
    {
        private readonly LinkDataPool<TId> _linkDatasPool;
        private readonly ICostProvider<TNodeData> _costProvider;


        public LinkDataFactory(LinkDataPool<TId> linkDatasPool, ICostProvider<TNodeData> costProvider)
        {
            _linkDatasPool = linkDatasPool;
            _costProvider = costProvider;
        }

        public IEnumerable<LinkData<TId>> CreateLinksFromNode(TNodeData from, IEnumerable<TNodeData> neighbours)
        {
            foreach (var to in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public IEnumerable<LinkData<TId>> CreateLinksToNode(TNodeData to, IEnumerable<TNodeData> neighbours)
        {
            foreach (var from in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public LinkData<TId> CreateLink(TNodeData from, TNodeData to)
        {
            var cost = _costProvider.GetCost(from, to);

            return CreateLinkInternal(from.Id, to.Id, cost);
        }

        private LinkData<TId> CreateLinkInternal(TId fromId, TId toId, float cost)
        {            
            var linkData = _linkDatasPool.Spawn(fromId, toId, cost);

            return linkData;
        }

        public void DeleteItem(LinkData<TId> item)
        {
            _linkDatasPool.Despawn(item);
        }
    }
}