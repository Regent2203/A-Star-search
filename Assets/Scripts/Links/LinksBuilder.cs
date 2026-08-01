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
        where L : ILinkData<TId>
    {        

        private readonly ILinksFactory<T, L, TId> _linksFactory;
        private readonly StoredLinksProvider<T, L, TId> _linksProvider;

        private readonly DictTypeStorage<L, LinkKey<TId>> _linkDatas;
        private readonly DictTypeStorage<LinkView<TId>, LinkKey<TId>> _linkViews;
        //private readonly LinkDataPool<TId> _linkDatasPool; //todo
        private readonly LinkViewPool<TId> _linkViewsPool;
        private readonly IObjectsStorage<V, TId> _nodeViews;


        public LinksBuilder(ILinksFactory<T, L, TId> linksFactory, StoredLinksProvider<T, L, TId> linksProvider,
            DictTypeStorage<L, LinkKey<TId>> linkDatas, DictTypeStorage<LinkView<TId>, LinkKey<TId>> linkViews,
            LinkViewPool<TId> viewsPool,
            IObjectsStorage<V, TId> nodeViews)
        {
            _linksFactory = linksFactory;
            _linksProvider = linksProvider;

            _linkDatas = linkDatas;
            _linkViews = linkViews;
            _linkViewsPool = viewsPool;
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
    }
}