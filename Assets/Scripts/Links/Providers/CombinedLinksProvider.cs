using EasyField.Nodes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyField.Links.Providers
{
    public class CombinedLinksProvider<TNodeData, TLinkData> : ILinksProvider<TLinkData, Vector2Int>
        where TNodeData : INodeData<Vector2Int>
        where TLinkData : ILinkData<Vector2Int>
    {
        private readonly StoredLinksProvider<TLinkData, Vector2Int> _storedLinksProvider;
        private readonly GridDynamicLinksProvider<TNodeData, TLinkData> _gridDynamicLinksProvider;


        public CombinedLinksProvider(StoredLinksProvider<TLinkData, Vector2Int> storedLinksProvider, 
            GridDynamicLinksProvider<TNodeData, TLinkData> gridDynamicLinksProvider)
        {
            _storedLinksProvider = storedLinksProvider;
            _gridDynamicLinksProvider = gridDynamicLinksProvider;
        }

        public bool TryGetLink(Vector2Int fromId, Vector2Int toId, out TLinkData link)
        {
            return _storedLinksProvider.TryGetLink(fromId, toId, out link);
        }

        public void AddLink(TLinkData link)
        {
            _storedLinksProvider.AddLink(link);
        }

        public void RemoveLink(DualKey<Vector2Int> key)
        {
            _storedLinksProvider.RemoveLink(key);
        }

        public void ClearAllLinks()
        {
            _storedLinksProvider.ClearAllLinks();
        }

        public IEnumerable<TLinkData> GetLinksFromNode(Vector2Int id)
        {
            var storedLinks = _storedLinksProvider.GetLinksFromNode(id);
            var dynamicLinks = _gridDynamicLinksProvider.GetLinksFromNode(id);
            
            return storedLinks.Concat(dynamicLinks);
        }

        public IEnumerable<TLinkData> GetLinksToNode(Vector2Int id)
        {
            var storedLinks = _storedLinksProvider.GetLinksToNode(id);
            var dynamicLinks = _gridDynamicLinksProvider.GetLinksToNode(id);
            
            return storedLinks.Concat(dynamicLinks);
        }
    }
}