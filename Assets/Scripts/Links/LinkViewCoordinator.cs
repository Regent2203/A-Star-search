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
        private readonly IObjectsStorage<TNodeView, TId> _nodeViews;
        private readonly DictTypeStorage<LinkView<TId>, LinkKey<TId>> _linkViews;


        public LinkViewCoordinator(IObjectsStorage<TNodeView, TId> nodeViews, DictTypeStorage<LinkView<TId>, LinkKey<TId>> linkViews) 
        {
            _nodeViews = nodeViews;
            _linkViews = linkViews;
        }

        public void CheckSingle(LinkView<TId> linkView)
        {
            var viewFrom = _nodeViews.GetItem(linkView.From);
            var viewTo = _nodeViews.GetItem(linkView.To);

            Vector2 posFrom = viewFrom.GetCenterCoords();
            Vector2 posTo = viewTo.GetCenterCoords();

            linkView.UpdatePositions(posFrom, posTo);            
        }

        public void CheckDual(LinkView<TId> linkView, bool isDelete)
        {
            var oppKey = new LinkKey<TId>(linkView.To, linkView.From);
            if (_linkViews.TryGetItem(oppKey, out var oppLinkView)) //if opposite-directed link found
            {
                if (!isDelete)
                {
                    linkView.ChangePlacementType(PlacementType.Left);                    
                    oppLinkView.ChangePlacementType(PlacementType.Left);                    
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
