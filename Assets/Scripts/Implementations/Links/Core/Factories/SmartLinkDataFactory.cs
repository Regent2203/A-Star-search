using EasyField.Links.CostProviders;
using EasyField.Nodes;
using System.Collections.Generic;

namespace EasyField.Links.Factories
{
    public class SmartLinkDataFactory<TNodeData, TLinkData, TId>
        where TNodeData : INodeData<TId>
        where TLinkData : ILinkData<TId>
    {
        private readonly ILinkDataFactory<TLinkData, TId> _factory;
        private readonly ICostProvider<TNodeData> _costProvider;


        public SmartLinkDataFactory(ILinkDataFactory<TLinkData, TId> factory, ICostProvider<TNodeData> costProvider)
        {
            _factory = factory;
            _costProvider = costProvider;
        }

        public IEnumerable<TLinkData> CreateLinksFromNode(TNodeData from, IEnumerable<TNodeData> neighbours)
        {
            foreach (var to in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public IEnumerable<TLinkData> CreateLinksToNode(TNodeData to, IEnumerable<TNodeData> neighbours)
        {
            foreach (var from in neighbours)
            {
                yield return CreateLink(from, to);
            }
        }

        public TLinkData CreateLink(TNodeData from, TNodeData to, float? cost = null)
        {
            float linkCost = cost ?? GetCost(from, to);
            
            return _factory.CreateItem(from.Id, to.Id, linkCost);
        }

        private float GetCost(TNodeData from, TNodeData to)
        {
            return _costProvider.GetCost(from, to);
        }

        public void DeleteLink(TLinkData item)
        {
            _factory.DeleteItem(item);
        }
    }
}