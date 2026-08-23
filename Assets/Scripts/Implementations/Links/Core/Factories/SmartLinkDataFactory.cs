using System.Collections.Generic;
using EasyField.Links.CostProviders;
using EasyField.Links.Implementations;
using EasyField.Nodes;
using UnityEngine;

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

        public LinkData<TId> CreateLink(TNodeData from, TNodeData to, float? cost = null)
        {
            float linkCost = cost ?? GetCost(from, to);

            return CreateItem(from.Id, to.Id, linkCost);
        }

        private float GetCost(TNodeData from, TNodeData to)
        {
            //todo is diagonal
            return _costProvider.GetCost(from, to);
        }
    }

    //todo
    public interface IDiag<TNodeData, TId>
        where TNodeData : INodeData<TId>
    {
        public void IsDiag(TNodeData from, TNodeData to);
    }

    public class Diag<TNodeData>
        where TNodeData : INodeData<Vector2Int>
    {
        public bool IsDiag(TNodeData from, TNodeData to)
        {
            return Mathf.Abs(from.Id.x - to.Id.x) == 1 && Mathf.Abs(from.Id.y - to.Id.y) == 1;
        }
    }
}