using EasyField.Nodes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyField.Links.Providers
{
    public class CombinedLinksProvider<TNodeData> : ILinksProvider<ILinkData<Vector2Int>, Vector2Int>
        where TNodeData : INodeData<Vector2Int>
    {
        private readonly StoredLinksProvider<ILinkData<Vector2Int>, Vector2Int> _storedLinksProvider;
        private readonly GridDynamicLinksProvider<TNodeData> _gridDynamicLinksProvider;


        public CombinedLinksProvider(StoredLinksProvider<ILinkData<Vector2Int>, Vector2Int> storedLinksProvider, GridDynamicLinksProvider<TNodeData> gridDynamicLinksProvider)
        {
            _storedLinksProvider = storedLinksProvider;
            _gridDynamicLinksProvider = gridDynamicLinksProvider;
        }

        public IEnumerable<ILinkData<Vector2Int>> GetLinksFromNode(Vector2Int id)
        {
            var storedLinks = _storedLinksProvider.GetLinksFromNode(id);
            var dynamicLinks = _gridDynamicLinksProvider.GetLinksFromNode(id);
            var result = storedLinks.Concat(dynamicLinks);
            Debug.Log($"result from {id}: {result.Count()}");
            return result;
        }

        public IEnumerable<ILinkData<Vector2Int>> GetLinksToNode(Vector2Int id)
        {
            var storedLinks = _storedLinksProvider.GetLinksToNode(id);
            var dynamicLinks = _gridDynamicLinksProvider.GetLinksToNode(id);
            var result = storedLinks.Concat(dynamicLinks);
            Debug.Log($"result to {id}: {result.Count()}");
            return result;
        }
    }
}