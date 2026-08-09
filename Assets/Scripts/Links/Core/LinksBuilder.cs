using EasyField.Links.Factories;
using EasyField.Links.Providers;
using EasyField.Links.ViewMovers;
using EasyField.Nodes;
using EasyField.ObjectsStorages;
using UnityEngine;

namespace EasyField.Links.Implementations
{
    public class LinksBuilder<TNodeData, TNodeView, TId>
        where TNodeData : class, INodeData<TId>
        where TNodeView : MonoBehaviour, INodeView<TId>
    {        

        private readonly LinkDataFactory<TNodeData, TId> _linkDatasFactory;
        private readonly LinkViewFactory<TId> _linkViewsFactory;
        private readonly StoredLinksProvider<LinkData<TId>, TId> _linksProvider;
        private readonly LinkViewCoordinator<TNodeView, TId> _linkViewCoordinator;

        private readonly DictTypeStorage<LinkData<TId>, LinkKey<TId>> _linkDatas;
        private readonly DictTypeStorage<LinkView<TId>, LinkKey<TId>> _linkViews;


        public LinksBuilder(LinkDataFactory<TNodeData, TId> linkDatasFactory, LinkViewFactory<TId> linkViewsFactory,
            StoredLinksProvider<LinkData<TId>, TId> linksProvider, LinkViewCoordinator<TNodeView, TId> linkViewCoordinator,
            DictTypeStorage<LinkData<TId>, LinkKey<TId>> linkDatas, DictTypeStorage<LinkView<TId>, LinkKey<TId>> linkViews)
        {
            _linkDatasFactory = linkDatasFactory;
            _linkViewsFactory = linkViewsFactory;
            _linksProvider = linksProvider;
            _linkViewCoordinator = linkViewCoordinator;

            _linkDatas = linkDatas;
            _linkViews = linkViews;
        }

        public bool TryCreateLink(TNodeData from, TNodeData to)
        {
            if (ValidateSameNode(from, to)) 
                return false;

            if (_linksProvider.TryGetLink(from.Id, to.Id, out _))
                return false;

            var key = new LinkKey<TId>(from.Id, to.Id);
            var linkData = _linkDatasFactory.CreateLink(from, to);
            var linkView = _linkViewsFactory.CreateItem(from.Id, to.Id, linkData.Cost, PlacementType.Center);
            _linkViewCoordinator.CheckDual(linkView, false);

            _linksProvider.AddLink(linkData);             
            _linkViews.AddItem(key, linkView);

            return true;            
        }

        public bool TryDeleteLink(TNodeData from, TNodeData to)
        {
            if (ValidateSameNode(from, to))
                return false;

            if (!_linksProvider.TryGetLink(from.Id, to.Id, out var linkData))
                return false;

            var key = new LinkKey<TId>(from.Id, to.Id);
            var linkView = _linkViews.GetItem(key);
            _linkViewCoordinator.CheckDual(linkView, true);

            _linksProvider.RemoveLink(linkData);
            _linkViews.RemoveItem(key);

            _linkDatasFactory.DeleteItem(linkData);
            _linkViewsFactory.DeleteItem(linkView);

            return true;
        }

        public void ClearAll()
        {
            foreach (var linkData in _linkDatas.AllItems)
            {
                _linkDatasFactory.DeleteItem(linkData);                
            }
            _linksProvider.ClearAllLinks();

            foreach (var linkView in _linkViews.AllItems)
            {
                _linkViewsFactory.DeleteItem(linkView);                
            }
            _linkViews.ClearData();
        }

        private bool ValidateSameNode(TNodeData from, TNodeData to)
        {
            return from == to;
        }
    }
}