using System.Collections.Generic;
using ThisProject.Links;
using ThisProject.Links.Factories;
using ThisProject.Links.Providers;
using ThisProject.Nodes;
using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.VisualLinks
{
    public class VisualLinksCreator<T, V, TId> : MonoBehaviour 
        where V: MonoBehaviour, INodeView
        where T: class, INodeData<TId>
    {
        private readonly Dictionary<ILinkData<TId>, VisualLink<T>> _activeVisualLinks = new Dictionary<ILinkData<TId>, VisualLink<T>>();

        private LinksFactory<T, TId> _linksFactory;
        private VisualLinksPool<T> _visualLinksPool;
        private StoredLinksProvider<T, TId> _linksProvider;

        
        [Inject]
        public void Construct(LinksFactory<T, TId> linksFactory, VisualLinksPool<T> visualLinksPool, StoredLinksProvider<T, TId> linksProvider)
        {
            _linksFactory = linksFactory;
            _visualLinksPool = visualLinksPool;
            _linksProvider = linksProvider;
        }

        public void TryCreateLink(T from, T to)
        {
            if (from == to) 
                return;

            var link = _linksFactory.CreateLink(from, to);
            if (_linksProvider.TryAddLink(link))
            {
                //todo factory
                var visualLink = _visualLinksPool.Get();
                //visualLink.Init

                _activeVisualLinks[link] = visualLink;
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
                    _visualLinksPool.Release(visualLink);
                    _activeVisualLinks.Remove(targetKey);
                }
            }
        }      
    }
}