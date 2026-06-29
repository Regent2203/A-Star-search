using System.Collections.Generic;
using ThisProject.Links;
using ThisProject.Links.Factories;
using ThisProject.Links.Providers;
using ThisProject.Nodes;
using UnityEngine;
using Zenject;

namespace ThisProject.Implementations.VisualLinks
{
    public class VisualLinksCreator<T, V> : MonoBehaviour 
        where V: MonoBehaviour, INodeView
        where T: class, INodeData
    {
        private readonly Dictionary<ILinkData<T>, VisualLink<T>> _activeVisualLinks = new Dictionary<ILinkData<T>, VisualLink<T>>();

        private LinksFactory<T> _linksFactory;
        private VisualLinksPool<T> _visualLinksPool;
        private StoredLinksProvider<T> _linksProvider;


        [Inject]
        public void Construct(LinksFactory<T> linksFactory, VisualLinksPool<T> visualLinksPool, StoredLinksProvider<T> linksProvider)
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
                var visualLink = _visualLinksPool.Get();
                //visualLink.Bind(link); //todo

                _activeVisualLinks[link] = visualLink;
            }
        }

        public void TryDeleteLink(T from, T to)
        {
            if (_linksProvider.TryRemoveLink(from, to))
            {
                ILinkData<T> targetKey = null;

                foreach (var key in _activeVisualLinks.Keys)
                {
                    if (key.From == from && key.To == to)
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