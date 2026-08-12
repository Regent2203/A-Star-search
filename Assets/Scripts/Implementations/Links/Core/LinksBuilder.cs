using EasyField.Links.Factories;
using EasyField.Links.Providers;
using EasyField.Links.ViewMovers;
using EasyField.Nodes;
using EasyField.ObjectsStorages;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EasyField.Links.Implementations
{
    public class LinksBuilder<TNodeData, TNodeView, TId>
        where TNodeData : class, INodeData<TId>
        where TNodeView : MonoBehaviour, INodeView<TId>
    {        

        private readonly SmartLinkDataFactory<TNodeData, TId> _linkDatasFactory;
        private readonly LinkViewFactory<TId> _linkViewsFactory;
        private readonly StoredLinksProvider<LinkData<TId>, TId> _linksProvider;
        private readonly LinkViewCoordinator<TNodeView, TId> _linkViewCoordinator;

        private readonly DictTypeStorage<LinkData<TId>, DualKey<TId>> _linkDatas;
        private readonly DictTypeStorage<LinkView<TId>, DualKey<TId>> _linkViews;


        public LinksBuilder(SmartLinkDataFactory<TNodeData, TId> linkDatasFactory, LinkViewFactory<TId> linkViewsFactory,
            StoredLinksProvider<LinkData<TId>, TId> linksProvider, LinkViewCoordinator<TNodeView, TId> linkViewCoordinator,
            DictTypeStorage<LinkData<TId>, DualKey<TId>> linkDatas, DictTypeStorage<LinkView<TId>, DualKey<TId>> linkViews)
        {
            _linkDatasFactory = linkDatasFactory;
            _linkViewsFactory = linkViewsFactory;
            _linksProvider = linksProvider;
            _linkViewCoordinator = linkViewCoordinator;

            _linkDatas = linkDatas;
            _linkViews = linkViews;
        }

        public bool TryCreateLinkItem(TNodeData from, TNodeData to)
        {
            if (ValidateSameNode(from.Id, to.Id)) 
                return false;

            if (_linksProvider.TryGetLink(from.Id, to.Id, out _))
                return false;

            CreateLinkItem(from, to);            

            return true;            
        }

        public bool TryDeleteLinkItem(TId fromId, TId toId)
        {
            if (ValidateSameNode(fromId, toId))
                return false;

            if (!_linksProvider.TryGetLink(fromId, toId, out var linkData))
                return false;

            DeleteLinkItem(fromId, toId);

            return true;
        }

        private void CreateLinkItem(TNodeData from, TNodeData to)
        {
            var key = new DualKey<TId>(from.Id, to.Id);
            var linkData = _linkDatasFactory.CreateLink(from, to);
            var linkView = _linkViewsFactory.CreateItem(from.Id, to.Id, linkData.Cost, PlacementType.Center);
            _linkViewCoordinator.CheckDual(linkView, false);

            _linksProvider.AddLink(linkData);
            _linkViews.AddItem(key, linkView);
        }

        private void DeleteLinkItem(TId fromId, TId toId)
        {
            var key = new DualKey<TId>(fromId, toId);
            var linkData = _linkDatas.GetItem(key);
            var linkView = _linkViews.GetItem(key);
            _linkViewCoordinator.CheckDual(linkView, true);

            _linksProvider.RemoveLink(key);
            _linkViews.RemoveItem(key);

            _linkDatasFactory.DeleteItem(linkData);
            _linkViewsFactory.DeleteItem(linkView);
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

        public void DeleteLinksFromNode(TId id)
        {
            foreach (var linkdata in _linksProvider.GetLinksFromNode(id).ToList())
            {
                DeleteLinkItem(linkdata.From, linkdata.To);
            }
        }

        public void DeleteLinksToNode(TId id)
        {
            foreach (var linkdata in _linksProvider.GetLinksToNode(id).ToList())
            {
                DeleteLinkItem(linkdata.From, linkdata.To);
            }
        }

        private bool ValidateSameNode(TId fromId, TId toId)
        {
            return EqualityComparer<TId>.Default.Equals(fromId, toId);
        }
    }
}