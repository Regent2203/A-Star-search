using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links.Providers
{
    public class StoredLinksProvider<T> : ILinksProvider<T> where T : class, INode
    {
        private readonly Dictionary<T, Dictionary<T, ILink<T>>> _links = new Dictionary<T, Dictionary<T, ILink<T>>>(); //_links[From][To]

        public bool TryAddLink(ILink<T> link)
        {
            if (link == null) 
                return false;

            var fromNode = link.From;
            var toNode = link.To;

            if (!_links.TryGetValue(fromNode, out var linksFrom))
            {
                linksFrom = new Dictionary<T, ILink<T>>();
                _links[fromNode] = linksFrom;
            }

            if (!linksFrom.ContainsKey(toNode)) //only one link between two nodes is allowed
            {
                linksFrom[toNode] = link;
                return true;
            }
            else
                return false;
        }

        public bool TryRemoveLink(T from, T to)
        {
            if (from == null || to == null) 
                return false;

            if (!_links.TryGetValue(from, out var linksFrom))
                return false;

            bool removed = linksFrom.Remove(to);

            if (linksFrom.Count == 0)
            {
                _links.Remove(from);
            }

            return removed;
        }

        public IEnumerable<ILink<T>> GetLinksForNode(T node)
        {
            if (_links.TryGetValue(node, out var fromLinks))
                 return fromLinks.Values;
            else
                return null;
        }
    }
}
