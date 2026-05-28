using ThisProject.Nodes;
using System.Collections.Generic;
using System.Linq;

namespace ThisProject.Links.Providers
{
    public class StoredLinksProvider<T> : ILinksProvider<T> where T : class, INode
    {
        private readonly Dictionary<T, Dictionary<T, ILink<T>>> _fromLinks = new Dictionary<T, Dictionary<T, ILink<T>>>();
        private readonly Dictionary<T, Dictionary<T, ILink<T>>> _toLinks = new Dictionary<T, Dictionary<T, ILink<T>>>();


        public bool TryAddLink(ILink<T> link)
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

            if (!_fromLinks.TryGetValue(fromNode, out var outgoing))
            {
                outgoing = new Dictionary<T, ILink<T>>();
                _fromLinks[fromNode] = outgoing;
            }
            outgoing[toNode] = link;

            if (!_toLinks.TryGetValue(toNode, out var incoming))
            {
                incoming = new Dictionary<T, ILink<T>>();
                _toLinks[toNode] = incoming;
            }
            incoming[fromNode] = link;

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

        public IEnumerable<ILink<T>> GetLinksFromNode(T node)
        {
            if (node != null && _fromLinks.TryGetValue(node, out var links))
                return links.Values;

            return Enumerable.Empty<ILink<T>>();
        }

        public IEnumerable<ILink<T>> GetLinksToNode(T node)
        {
            if (node != null && _toLinks.TryGetValue(node, out var links))
                return links.Values;

            return Enumerable.Empty<ILink<T>>();
        }
    }
}
