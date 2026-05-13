using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links.Providers
{
    public class StoredLinksProvider<T> : ILinksProvider<T> where T : class, INode
    {
        private readonly Dictionary<T, List<ILink<T>>> _links = new Dictionary<T, List<ILink<T>>>();

        public bool AddLink(ILink<T> link)
        {
            if (link == null) 
                return false;

            var fromNode = link.From;

            if (!_links.TryGetValue(fromNode, out var linksList))
            {
                linksList = new List<ILink<T>>();
                _links[fromNode] = linksList;
            }

            if (!linksList.Contains(link)) //only one link between two nodes is allowed
            {
                linksList.Add(link);
                return true;
            }
            else
                return false;
        }

        public bool RemoveLink(ILink<T> link)
        {
            if (link == null) 
                return false;

            var fromNode = link.From;

            if (!_links.TryGetValue(fromNode, out var linksList))
                return false;

            bool removed = linksList.Remove(link);

            if (linksList.Count == 0)
            {
                _links.Remove(fromNode);
            }

            return removed;
        }

        public IEnumerable<ILink<T>> GetLinksForNode(T node)
        {
            if (_links.TryGetValue(node, out var links))
                return links;
            else
                return null;
        }
    }
}
