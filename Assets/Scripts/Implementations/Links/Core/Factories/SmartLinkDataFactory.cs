using System.Collections.Generic;
using EasyField.Links.CostProviders;
using EasyField.Links.Implementations;
using EasyField.Nodes;

namespace EasyField.Links.Factories
{
    public class SmartLinkDataFactory<TNodeData, TId> : LinkDataFactory<TId>
        where TNodeData : INodeData<TId>
    {
        private readonly ICostProvider<TNodeData> _costProvider;


        public SmartLinkDataFactory(LinkDataPool<TId> linkDatasPool, ICostProvider<TNodeData> costProvider) : base(linkDatasPool)
        {
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
            var cost = GetCost(from, to);

            return CreateItem(from.Id, to.Id, cost);
        }

        public float GetCost(TNodeData from, TNodeData to)
        {
            return _costProvider.GetCost(from, to);
        }
    }
}