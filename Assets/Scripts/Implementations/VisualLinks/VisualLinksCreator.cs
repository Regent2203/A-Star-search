using Core.Implementations.VisualLinks;
using Core.Links;
using Core.Links.Factories;
using Core.Links.Providers;
using Core.Nodes;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using Zenject;

namespace Core
{
    public class VisualLinksCreator<T> : MonoBehaviour where T: class, INode
    {
        private enum Mode { None, CreateLink, RemoveLink }

        private KeyCode _linkingKeyCode = KeyCode.LeftAlt;
        private Mode _mode = Mode.None;
        private T _firstNode;
        private readonly Dictionary<ILink<T>, VisualLink<T>> _activeVisualLinks = new Dictionary<ILink<T>, VisualLink<T>>();

        private LinksFactory<T> _linksFactory;
        private VisualLinksPool<T> _visualLinksPool;
        private StoredLinksProvider<T> _linksProvider;


        [Inject]
        public void Construct(LinksFactory<T> linksFactory, VisualLinksPool<T> visualLinksPool, StoredLinksProvider<T> linksProvider, [Inject(Id = "LinkingKey")] KeyCode linkingKeyCode)
        {
            _linksFactory = linksFactory;
            _visualLinksPool = visualLinksPool;
            _linksProvider = linksProvider;
            _linkingKeyCode = linkingKeyCode;
        }

        public void TryUseNode(T node, PointerEventData.InputButton btn)
        {
            bool isLinkingMode = Input.GetKey(_linkingKeyCode);

            if (!isLinkingMode)
                Cancel();
            else
            switch (_mode)
            //[alt + lmb] twice -> CreateLink
            //[alt + rmb] twice -> DeleteLink
            {
                case Mode.None:
                    {
                        if (btn == PointerEventData.InputButton.Left) //lmb
                        {
                            _firstNode = node;
                            _mode = Mode.CreateLink;
                        }
                        else if (btn == PointerEventData.InputButton.Right) //rmb
                        {
                            _firstNode = node;
                            _mode = Mode.RemoveLink;
                        }
                    }
                    break;
                case Mode.CreateLink:
                    {
                        if (btn == PointerEventData.InputButton.Left) //lmb
                        {
                            TryCreateLink(_firstNode, node);
                            Cancel();
                        }
                        else if (btn == PointerEventData.InputButton.Right) //rmb
                        {
                            Cancel();
                        }
                    }
                    break;
                case Mode.RemoveLink:
                    {
                        if (btn == PointerEventData.InputButton.Right) //rmb
                        {
                            TryDeleteLink(_firstNode, node);
                            Cancel();
                        }
                        else if (btn == PointerEventData.InputButton.Left) //lmb
                        {
                            Cancel();
                        }
                    }
                    break;
            }
        }

        private void TryCreateLink(T from, T to)
        {
            if (from == to) 
                return;

            var link = _linksFactory.CreateLink(from, to);
            if (_linksProvider.TryAddLink(link))
            {
                var visualLink = _visualLinksPool.Get();
                visualLink.Bind(link);

                _activeVisualLinks[link] = visualLink;
            }
        }

        private void TryDeleteLink(T from, T to)
        {
            if (_linksProvider.TryRemoveLink(from, to))
            {
                ILink<T> targetKey = null;

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

        private void Cancel()
        {
            _mode = Mode.None; 
            _firstNode = null;
        }        
    }
}