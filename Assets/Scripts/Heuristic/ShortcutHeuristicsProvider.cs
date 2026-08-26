using EasyField.Links;
using EasyField.Links.Providers;
using EasyField.Nodes;
using EasyField.ObjectsStorages;
using System;

namespace EasyField.Heuristic
{
    public class ShortcutHeuristicsProvider<TNodeData, TLinkData, TId> : IHeuristicsProvider<TNodeData>
        where TNodeData : INodeData
        where TLinkData : ILinkData<TId>
    {
        private readonly IHeuristicsProvider<TNodeData> _heuristicsProvider;
        private readonly StoredLinksProvider<TLinkData, TId> _linksProvider;
        private readonly IObjectsStorage<TNodeData, TId> _nodeDatasStorage;


        public ShortcutHeuristicsProvider(IHeuristicsProvider<TNodeData> heuristicsProvider, StoredLinksProvider<TLinkData, TId> linksProvider,
            IObjectsStorage<TNodeData, TId> nodeDatasStorage)
        {
            _heuristicsProvider = heuristicsProvider;
            _linksProvider = linksProvider;
            _nodeDatasStorage = nodeDatasStorage;
        }

        public float EstimateCost(TNodeData from, TNodeData to)
        {
            var minCost = _heuristicsProvider.EstimateCost(from, to);
            var shortcutLinks = _linksProvider.GetAllLinks();

            TNodeData linkFrom, linkTo;

            foreach (var link in shortcutLinks)
            {
                linkFrom = _nodeDatasStorage.GetItem(link.From);
                linkTo = _nodeDatasStorage.GetItem(link.To);

                var shortcutCost = _heuristicsProvider.EstimateCost(from, linkFrom) + link.Cost + _heuristicsProvider.EstimateCost(linkTo, to);

                if (shortcutCost < minCost)
                {
                    minCost = shortcutCost;
                }
            }

            return minCost;
        }
    }
}