using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Links.ViewMovers
{
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
