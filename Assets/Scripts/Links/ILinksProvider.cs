using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links
{
    public interface ILinksProvider<T> where T: INode<T>
    {
        public IEnumerable<ILink<T>> GetLinks(T from, IEnumerable<T> neighbours);
    }
}
