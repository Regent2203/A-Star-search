using ThisProject.Links.Factories;
using ThisProject.Links.Providers;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Links
{
    public class LinksBuilder<T, V, L, TId>
        where T : class, INodeData<TId>
        where V : MonoBehaviour, INodeView<TId>
    {        

        private readonly ILinksFactory<T, LinkData<TId>, TId> _linksFactory;
        private readonly StoredLinksProvider<T, LinkData<TId>, TId> _linksProvider;

        private readonly DictTypeStorage<LinkData<TId>, LinkKey<TId>> _linkDatas;
        private readonly DictTypeStorage<LinkView<TId>, LinkKey<TId>> _linkViews;
        private readonly LinkDataPool<TId> _linkDatasPool;
        private readonly LinkViewPool<TId> _linkViewsPool;
        private readonly IObjectsStorage<V, TId> _nodeViews;


        public LinksBuilder(ILinksFactory<T, LinkData<TId>, TId> linksFactory, StoredLinksProvider<T, LinkData<TId>, TId> linksProvider,
            DictTypeStorage<LinkData<TId>, LinkKey<TId>> linkDatas, DictTypeStorage<LinkView<TId>, LinkKey<TId>> linkViews,
            LinkDataPool<TId> linkDatasPool, LinkViewPool<TId> linkViewsPool,
            IObjectsStorage<V, TId> nodeViews)
        {
            _linksFactory = linksFactory;
            _linksProvider = linksProvider;

            _linkDatas = linkDatas;
            _linkDatasPool = linkDatasPool;
            _linkViews = linkViews;
            _linkViewsPool = linkViewsPool;

            _nodeViews = nodeViews;
        }

        public bool TryCreateLink(T from, T to)
        {
            if (from == to) 
                return false;

            var linkData = _linksFactory.CreateLink(from, to);

            if (_linksProvider.TryAddLink(linkData))
            {
                var linkKey = new LinkKey<TId>(from.Id, to.Id);
                var fromView = _nodeViews.GetItem(from.Id);
                var toView = _nodeViews.GetItem(to.Id);

                var linkView = _linkViewsPool.Spawn(fromView, toView, PlacementType.Center);                
                _linkViews.AddItem(linkKey, linkView);
                return true;
            }
            else
                return false;
        }

        public bool TryDeleteLink(T from, T to)
        {
            if (from == to)
                return false;

            if (_linksProvider.TryRemoveLink(from.Id, to.Id))
            {
                var linkKey = new LinkKey<TId>(from.Id, to.Id);
                var linkView = _linkViews.GetItem(linkKey);

                _linkViewsPool.Despawn(linkView);
                _linkViews.RemoveItem(linkKey);
                return true;
            }
            else
                return false;
        }

        public void ClearAll()
        {            
            foreach (var data in _linkDatas.AllItems)
            {
                _linkDatasPool.Despawn(data);
                //_linksProvider.TryRemoveLink(data.From, data.To);
            }
            _linkDatas.ClearData();

            foreach (var view in _linkViews.AllItems)
            {
                _linkViewsPool.Despawn(view);
            }
            _linkViews.ClearData();
        }
    }
}