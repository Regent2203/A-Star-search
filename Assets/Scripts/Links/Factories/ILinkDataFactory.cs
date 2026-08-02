using System.Collections.Generic;
using ThisProject.Nodes;

namespace ThisProject.Links.Factories
{
    public interface ILinkDataFactory<TNodeData, TLinkData, TId>
        where TNodeData : INodeData<TId>
        where TLinkData : ILinkData<TId>
    {
        public IEnumerable<TLinkData> CreateLinksFromNode(TNodeData from, IEnumerable<TNodeData> neighbours);
        public IEnumerable<TLinkData> CreateLinksToNode(TNodeData to, IEnumerable<TNodeData> neighbours);
        public TLinkData CreateLink(TNodeData from, TNodeData to);
    }
}