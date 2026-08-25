using EasyField.Nodes;
using EasyField.ObjectsStorages;
using UnityEngine;

namespace EasyField.Links.ViewMovers
{
    /// <summary>
    /// Updates start and end positions of LinkView.
    /// When there are two opposited-directed links between same nodes, their visuals will overlap.
    /// If you are fine with that, use CheckSingle() method only.
    /// If you want to offset visuals for such case, use CheckDual() method instead.
    /// </summary>
    public class LinkViewCoordinator<TNodeView, TId>
        where TNodeView : INodeView<TId>
    {
        private readonly PlacementType _dualModePlacementType;

        private readonly IObjectsStorage<TNodeView, TId> _nodeViews;
        private readonly DictTypeStorage<LinkView<TId>, DualKey<TId>> _linkViews;


        public LinkViewCoordinator(IObjectsStorage<TNodeView, TId> nodeViews, DictTypeStorage<LinkView<TId>, DualKey<TId>> linkViews,
            PlacementType dualModePlacementType = PlacementType.Left)
        {
            _nodeViews = nodeViews;
            _linkViews = linkViews;

            _dualModePlacementType = dualModePlacementType;
        }

        /// <summary>
        /// Refreshes parameters of LinkView. Doesn't check the need for dual-mode offset (two oppositely directed LinkViews will overlap)
        /// </summary>
        public void CheckSingle(LinkView<TId> linkView)
        {
            var viewFrom = _nodeViews.GetItem(linkView.From);
            var viewTo = _nodeViews.GetItem(linkView.To);

            Vector2 posFrom = viewFrom.GetCenterCoords();
            Vector2 posTo = viewTo.GetCenterCoords();

            linkView.UpdatePositions(posFrom, posTo);            
        }

        /// <summary>
        /// Refreshes parameters of LinkView, and checks the need for dual-mode offset (two oppositely directed LinkViews will be parallel)
        /// </summary>
        /// <param name="isDelete">Use True when deleting link and False when creating link.</param>
        public void CheckDual(LinkView<TId> linkView, bool isDelete)
        {
            var oppKey = new DualKey<TId>(linkView.To, linkView.From);
            if (_linkViews.TryGetItem(oppKey, out var oppLinkView)) //if opposite-directed link found
            {
                if (!isDelete)
                {
                    linkView.ChangePlacementType(_dualModePlacementType);                    
                    oppLinkView.ChangePlacementType(_dualModePlacementType);                    
                }
                else
                {
                    oppLinkView.ChangePlacementType(PlacementType.Center);                    
                }
                CheckSingle(oppLinkView);
            }
            CheckSingle(linkView);
        }
    }
}
