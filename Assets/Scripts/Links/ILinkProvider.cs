using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links
{
    public interface ILinkProvider
    {
        public IEnumerable<ILink> GetLinks(INode from, IEnumerable<INode> neighbours);
    }
}
