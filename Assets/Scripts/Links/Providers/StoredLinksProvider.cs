using ThisProject.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace ThisProject.Links.Providers
{
    public class StoredLinksProvider<T> : ILinksProvider<T>
        where T : INodeData
    {
        private readonly Dictionary<T, Dictionary<T, ILinkData<T>>> _fromLinks = new Dictionary<T, Dictionary<T, ILinkData<T>>>();
        private readonly Dictionary<T, Dictionary<T, ILinkData<T>>> _toLinks = new Dictionary<T, Dictionary<T, ILinkData<T>>>();


        public bool TryAddLink(ILinkData<T> link)
        {
            if (link == null) 
                return false;

            var fromNode = link.From;
            var toNode = link.To;

            if (_fromLinks.TryGetValue(fromNode, out var toNodes) && toNodes.ContainsKey(toNode))
            {
                //if such link already exists (only one link between two nodes is allowed)
                return false;
            }

            if (!_fromLinks.TryGetValue(fromNode, out var fromSubDict))
            {
                fromSubDict = new Dictionary<T, ILinkData<T>>();
                _fromLinks[fromNode] = fromSubDict;
            }
            fromSubDict[toNode] = link;

            if (!_toLinks.TryGetValue(toNode, out var toSubDict))
            {
                toSubDict = new Dictionary<T, ILinkData<T>>();
                _toLinks[toNode] = toSubDict;
            }
            toSubDict[fromNode] = link;

            return true;
        }

        public bool TryRemoveLink(T from, T to)
        {
            if (from == null || to == null)
                return false;

            if (!_fromLinks.TryGetValue(from, out var outgoing) || !outgoing.ContainsKey(to))
            {
                //if such link doesn't exist
                return false;
            }

            outgoing.Remove(to);
            if (outgoing.Count == 0)
            {
                _fromLinks.Remove(from);
            }

            if (_toLinks.TryGetValue(to, out var incoming))
            {
                incoming.Remove(from);
                if (incoming.Count == 0)
                {
                    _toLinks.Remove(to);
                }
            }

            return true;
        }

        public IEnumerable<ILinkData<T>> GetLinksFromNode(T node)
        {
            if (node != null && _fromLinks.TryGetValue(node, out var links))
                return links.Values;

            return Enumerable.Empty<ILinkData<T>>();
        }

        public IEnumerable<ILinkData<T>> GetLinksToNode(T node)
        {
            if (node != null && _toLinks.TryGetValue(node, out var links))
                return links.Values;

            return Enumerable.Empty<ILinkData<T>>();
        }
    }
}
