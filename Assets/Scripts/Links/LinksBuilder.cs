using System.Collections.Generic;
using ThisProject.Links.Factories;
using ThisProject.Links.Providers;
using ThisProject.Nodes;
using ThisProject.ObjectsStorages;
using UnityEngine;

namespace ThisProject.Links
{
    public class LinksBuilder<T, V, TId>
        where V: MonoBehaviour, INodeView<TId>
        where T: class, INodeData<TId>
    {
        private readonly Dictionary<ILinkData<TId>, LinkView<TId>> _activeVisualLinks = new();

        private LinksFactory<T, TId> _linksFactory;
        private StoredLinksProvider<T, TId> _linksProvider;

        private readonly DictTypeStorage<LinkData<TId>, LinkKey<TId>> _linkDatas;
        private readonly DictTypeStorage<LinkView<TId>, LinkKey<TId>> _linkViews;
        //private readonly LinkDataPool _linksPool; //todo
        private readonly LinkViewPool<TId> _viewsPool;
        private readonly IObjectsStorage<V, TId> _nodeViews;


        public LinksBuilder(LinksFactory<T, TId> linksFactory, StoredLinksProvider<T, TId> linksProvider,
            DictTypeStorage<LinkData<TId>, LinkKey<TId>> links, DictTypeStorage<LinkView<TId>, LinkKey<TId>> views,
            LinkViewPool<TId> viewsPool,
            IObjectsStorage<V, TId> nodeViews)
        {
            _linksFactory = linksFactory;
            _linksProvider = linksProvider;

            _linkDatas = links;
            _linkViews = views;
            _viewsPool = viewsPool;
            _nodeViews = nodeViews;
        }

        public void TryCreateLink(T from, T to)
        {
            if (from == to) 
                return;

            var linkData = _linksFactory.CreateLink(from, to);

            if (_linksProvider.TryAddLink(linkData))
            {
                //var linkKey = new LinkKey<TId>(from.Id, to.Id);
                var fromView = _nodeViews.GetItem(from.Id);
                var toView = _nodeViews.GetItem(to.Id);

                var linkView = _viewsPool.Spawn(fromView, toView, PlacementType.Center);
                
                _activeVisualLinks[linkData] = linkView;
            }
        }

        public void TryDeleteLink(T from, T to)
        {
            if (_linksProvider.TryRemoveLink(from.Id, to.Id))
            {
                ILinkData<TId> targetKey = null;

                foreach (var key in _activeVisualLinks.Keys)
                {
                    if (EqualityComparer<TId>.Default.Equals(key.From, from.Id) &&
                        EqualityComparer<TId>.Default.Equals(key.To, to.Id))
                    {
                        targetKey = key;
                        break;
                    }
                }

                if (targetKey != null && _activeVisualLinks.TryGetValue(targetKey, out var visualLink))
                {
                    //todo
                    //_viewsPool.Release(visualLink);
                    //_activeVisualLinks.Remove(targetKey);
                }
            }
        }      
    }
}