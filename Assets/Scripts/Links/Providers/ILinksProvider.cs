using System;
using System.Collections.Generic;
using ThisProject.Nodes;

namespace ThisProject.Links.Providers
{
    public interface ILinksProvider<T, TId>
        where T : INodeData<TId>
        //where TId : IEquatable<TId>
    {
        public IEnumerable<ILinkData<TId>> GetLinksFromNode(T node);
        public IEnumerable<ILinkData<TId>> GetLinksToNode(T node);
    }
}
