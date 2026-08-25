using EasyField.Nodes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyField.Links.Providers
{
    public class CombinedLinksProvider<TNodeData, TLinkData> : ILinksProvider<TLinkData, Vector2Int>
        where TNodeData : INodeData<Vector2Int>
        where TLinkData : ILinkData<Vector2Int>, new()
    {
        private readonly StoredLinksProvider<TLinkData, Vector2Int> _storedLinksProvider;
        private readonly GridDynamicLinksProvider<TNodeData, TLinkData> _gridDynamicLinksProvider;


        public CombinedLinksProvider(StoredLinksProvider<TLinkData, Vector2Int> storedLinksProvider, 
            GridDynamicLinksProvider<TNodeData, TLinkData> gridDynamicLinksProvider)
        {
            _storedLinksProvider = storedLinksProvider;
            _gridDynamicLinksProvider = gridDynamicLinksProvider;
        }

        public IEnumerable<TLinkData> GetLinksFromNode(Vector2Int id)
        {
            var storedLinks = _storedLinksProvider.GetLinksFromNode(id);
            var dynamicLinks = _gridDynamicLinksProvider.GetLinksFromNode(id);
            var result = storedLinks.Concat(dynamicLinks);
            Debug.Log($"result from {id}: {result.Count()}");
            return result;
        }

        public IEnumerable<TLinkData> GetLinksToNode(Vector2Int id)
        {
            var storedLinks = _storedLinksProvider.GetLinksToNode(id);
            var dynamicLinks = _gridDynamicLinksProvider.GetLinksToNode(id);
            var result = storedLinks.Concat(dynamicLinks);
            Debug.Log($"result to {id}: {result.Count()}");
            return result;
        }
    }
}