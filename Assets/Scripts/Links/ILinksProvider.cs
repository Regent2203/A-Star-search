using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links
{
    public interface ILinksProvider
    {
        public IEnumerable<ILink> GetLinks(INode from, IEnumerable<INode> neighbours);
    }
}
