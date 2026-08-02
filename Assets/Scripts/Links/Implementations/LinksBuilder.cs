using ThisProject.Links.Factories;
using ThisProject.Links.Providers;
using ThisProject.Links.ViewMovers;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Links.Implementations
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
        private readonly LinkDataPool<TId> _linkDatasPool;
        private readonly LinkViewPool<TId> _linkViewsPool;
        private readonly IObjectsStorage<TNodeView, TId> _nodeViews;


        public LinksBuilder(LinkDataFactory<TNodeData, TId> linkDatasFactory, LinkViewFactory<TId> linkViewsFactory,
            StoredLinksProvider<LinkData<TId>, TId> linksProvider, LinkViewCoordinator<TNodeView, TId> linkViewCoordinator,
            DictTypeStorage<LinkData<TId>, LinkKey<TId>> linkDatas, DictTypeStorage<LinkView<TId>, LinkKey<TId>> linkViews,
            LinkDataPool<TId> linkDatasPool, LinkViewPool<TId> linkViewsPool,
            IObjectsStorage<TNodeView, TId> nodeViews)
        {
            _linkDatasFactory = linkDatasFactory;
            _linkViewsFactory = linkViewsFactory;
            _linksProvider = linksProvider;
            _linkViewCoordinator = linkViewCoordinator;

            _linkDatas = linkDatas;
            _linkDatasPool = linkDatasPool;
            _linkViews = linkViews;
            _linkViewsPool = linkViewsPool;

            _nodeViews = nodeViews;
        }

        public bool TryCreateLink(TNodeData from, TNodeData to)
        {
            if (ValidateSameNode(from, to)) 
                return false;

            if (_linksProvider.TryGetLink(from.Id, to.Id, out _))
                return false;

            var key = new LinkKey<TId>(from.Id, to.Id);
            var linkData = _linkDatasFactory.CreateLink(from, to);
            var linkView = _linkViewsFactory.CreateItem(from.Id, to.Id, PlacementType.Center);
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

            _linkDatasFactory.DeleteItem(linkData);
            _linkViewsFactory.DeleteItem(linkView);

            _linksProvider.RemoveLink(key);
            _linkViews.RemoveItem(key);            

            return true;
        }

        public void ClearAll()
        {
            //todo
            foreach (var linkData in _linkDatas.AllItems)
            {
                _linkDatasPool.Despawn(linkData);
                //_linksProvider.RemoveLink(linkData.From, linkData.To);
            }
            _linkDatas.ClearData();

            foreach (var linkView in _linkViews.AllItems)
            {
                _linkViewsPool.Despawn(linkView);
            }
            _linkViews.ClearData();
        }

        private bool ValidateSameNode(TNodeData from, TNodeData to)
        {
            return from == to;
        }
    }
}