using Core.Nodes;
using System.Collections.Generic;

namespace Core.Links.Providers
{
    public class StoredLinksProvider<T> : ILinksProvider<T> where T : class, INode
    {
        private readonly Dictionary<T, List<ILink<T>>> _links = new Dictionary<T, List<ILink<T>>>();
        /*
        private readonly ILinksFactory<T, TId> _factory;


        public StoredLinksProvider(ILinksFactory<T, TId> factory)
        {
            _factory = factory;
        }
        */
        public void AddLink()
        {
            //todo
            //_links.Add()
        }

        public void RemoveLink()
        {
            //todo
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
