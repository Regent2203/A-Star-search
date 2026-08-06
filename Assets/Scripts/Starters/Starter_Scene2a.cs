using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyField.Implementations.Vertexes;
using EasyField.Inputs;
using EasyField.Links;
using EasyField.Links.CostProviders;
using EasyField.Links.Implementations;
using EasyField.Links.LinkCostChangers;
using EasyField.Links.Providers;
using EasyField.Links.ViewMovers;
using EasyField.Nodes.NodeBlockers;
using EasyField.Nodes.ViewMovers;
using EasyField.Nodes.ViewSelectors;
using EasyField.PathDrawers;
using EasyField.PathFinders;
using EasyField.PathSetters;
using EasyField.SaveSystem;
using EasyField.UICommon;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.Starters
{
    public class Starter_Scene2a : StarterBase
    {
        private VertexDataStorage _nodeDatas;
        private VertexViewStorage _nodeViews;
        private LinkViewStorage_Int _linkViews;
        private VertexesClickHandler _clickHandler;
        private VertexesDragHandler _dragHandler;
        private VertexesFieldBuilder _builder;
        private ICostProvider<VertexData> _costProvider;
        private LinkCostSetter<LinkData<int>> _linkCostSetter;
        private NodeBlocker<VertexData> _nodeBlocker;
        private NodeViewSelector<VertexView> _viewSelector;
        private NodeViewMover<VertexView> _viewMover;
        private VertexesLinksBuilder _linksBuilder;
        private StoredLinksProvider<LinkData<int>, int> _linksProvider;
        private LinkViewCoordinator<VertexView, int> _linkViewCoordinator;
        private PathSetter<VertexData> _pathSetter;
        private PathFinder<VertexData, int> _pathFinder;
        private LinePathDrawer _pathDrawer;
        private ISaver _saver;
        private ILoader _loader;
        private VertexesFieldSaveDtoProvider _dtoProvider;
        //private UIVertexesHotkeyInfoPanel _hotkeyInfoPanel;
        private UISaveLoadPanel _saveLoadPanel;


        [Inject]
        public void Construct(VertexDataStorage nodes, VertexViewStorage views, LinkViewStorage_Int linkViews,
            VertexesClickHandler clickHandler, VertexesDragHandler dragHandler, VertexesFieldBuilder builder,
            ICostProvider<VertexData> costProvider, LinkCostSetter<LinkData<int>> linkCostSetter,
            NodeBlocker<VertexData> nodeBlocker, NodeViewSelector<VertexView> viewSelector, NodeViewMover<VertexView> viewMover, 
            VertexesLinksBuilder linksBuilder, StoredLinksProvider<LinkData<int>, int> linksProvider, LinkViewCoordinator<VertexView, int> linkViewCoordinator,
            PathSetter<VertexData> pathSetter, PathFinder<VertexData, int> pathFinder, LinePathDrawer pathDrawer,
            ISaver saver, ILoader loader, VertexesFieldSaveDtoProvider dtoProvider,
            UISaveLoadPanel saveLoadPanel)
        {
            _nodeDatas = nodes;
            _nodeViews = views;
            _linkViews = linkViews;
            _clickHandler = clickHandler;
            _dragHandler = dragHandler;
            _builder = builder;
            _costProvider = costProvider;
            _linkCostSetter = linkCostSetter;

            _nodeBlocker = nodeBlocker;
            _viewSelector = viewSelector;
            _viewMover = viewMover;

            _linksBuilder = linksBuilder;
            _linksProvider = linksProvider;
            _linkViewCoordinator = linkViewCoordinator;

            _pathSetter = pathSetter;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;

            _saver = saver;
            _loader = loader;
            _dtoProvider = dtoProvider;

            _saveLoadPanel = saveLoadPanel;
        }

        protected override void SubscribeAll()
        {
            _clickHandler.NodeViewClicked += OnViewClicked;
            _clickHandler.FieldClicked += OnFieldClicked;
            _dragHandler.NodeViewDragStarted += OnViewDragStarted;
            _dragHandler.NodeViewDragging += OnViewDragging;
            _dragHandler.NodeViewDragEnded += OnViewDragEnded;

            _viewSelector.ViewSelected += OnViewSelected;
            _viewMover.ViewMoved += OnViewMoved;
            _nodeBlocker.NodeBlocked += OnNodeBlocked;

            _pathSetter.StartNodeChanged += OnStartNodeChanged;
            _pathSetter.FinishNodeChanged += OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged += OnPathChanged;

            _saveLoadPanel.SaveBtnClicked += OnSaveBtnClicked;
            _saveLoadPanel.LoadBtnClicked += OnLoadBtnClicked;
        }

        protected override void InitDefaultStates()
        {
            //todo
            _builder.TestPopulate(5);
        }

        protected override void UnsubscribeAll()
        {
            _clickHandler.NodeViewClicked -= OnViewClicked;
            _clickHandler.FieldClicked -= OnFieldClicked;
            _dragHandler.NodeViewDragStarted -= OnViewDragStarted;
            _dragHandler.NodeViewDragging -= OnViewDragging;
            _dragHandler.NodeViewDragEnded -= OnViewDragEnded;

            _viewSelector.ViewSelected -= OnViewSelected;
            _viewMover.ViewMoved -= OnViewMoved;
            _nodeBlocker.NodeBlocked -= OnNodeBlocked;

            _pathSetter.StartNodeChanged -= OnStartNodeChanged;
            _pathSetter.FinishNodeChanged -= OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged -= OnPathChanged;

            _saveLoadPanel.SaveBtnClicked -= OnSaveBtnClicked;
            _saveLoadPanel.LoadBtnClicked -= OnLoadBtnClicked;
        }

        private void UpdateNodePosition(VertexView view, Vector2 pos)
        {
            var node = _nodeDatas.GetItem(view.Id);
            if (node.TryChangeNodePosition(pos))
                OnFieldChanged();
        }

        private void RedrawLinkViews(VertexView view)
        {
            LinkView<int> linkView;
            float cost;
            var fromLinks = _linksProvider.GetLinksFromNode(view.Id);
            var toLinks = _linksProvider.GetLinksToNode(view.Id);

            foreach (var linkData in fromLinks)
            {
                linkView = _linkViews.GetItem(new LinkKey<int>(view.Id, linkData.Id.To));
                _linkViewCoordinator.CheckSingle(linkView);

                //todo
                var from = _nodeDatas.GetItem(linkData.From);
                var to = _nodeDatas.GetItem(linkData.To);
                cost = _costProvider.GetCost(from, to);
                _linkCostSetter.SetLinkCost(linkData, cost);
                linkView.UpdateCostText(cost);
            }
            foreach (var linkData in toLinks)
            {
                linkView = _linkViews.GetItem(new LinkKey<int>(linkData.Id.From, view.Id));
                _linkViewCoordinator.CheckSingle(linkView);

                //todo
                var from = _nodeDatas.GetItem(linkData.From);
                var to = _nodeDatas.GetItem(linkData.To);
                cost = _costProvider.GetCost(from, to);
                _linkCostSetter.SetLinkCost(linkData, cost);
                linkView.UpdateCostText(cost);
            }
        }

        private void OnViewClicked(VertexView view, PointerEventData.InputButton button, InputSnapshot input)
        {
            var node = _nodeDatas.GetItem(view.Id);

            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                if (button == PointerEventData.InputButton.Left)
                    _viewSelector.SelectView(view);

                if (button == PointerEventData.InputButton.Right)
                    _nodeBlocker.TryBlockNode(node, !node.IsBlocked);
            }

            if (input.IsMarkingMode)
            {
                switch (button)
                {
                    case PointerEventData.InputButton.Left:
                        _pathSetter.UpdateStartNode(node);
                        break;
                    case PointerEventData.InputButton.Right:
                        _pathSetter.UpdateFinishNode(node);
                        break;
                }
            }

            if (input.IsLinkingMode)
            {
                if (_viewSelector.SelectedView != null)
                {
                    var selectedNode = _nodeDatas.GetItem(_viewSelector.SelectedView.Id);

                    switch (button)
                    {
                        case PointerEventData.InputButton.Left:
                            if (_linksBuilder.TryCreateLink(selectedNode, node))
                                OnFieldChanged();
                            break;
                        case PointerEventData.InputButton.Right:
                            if (_linksBuilder.TryDeleteLink(selectedNode, node))
                                OnFieldChanged();
                            break;
                    }                    
                }
            }

            if (input.IsCreatingMode)
            {
                if (button == PointerEventData.InputButton.Right)
                    _builder.DeleteNode(view);
            }
        }

        private void OnFieldClicked(Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            if (button == PointerEventData.InputButton.Left)
                _viewSelector.SelectView(null);

            if (input.IsCreatingMode)
                _builder.CreateNode(pos);
        }

        private void OnViewDragStarted(VertexView view, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            if ((button != PointerEventData.InputButton.Left) || 
                input.IsMarkingMode || input.IsCreatingMode || input.IsLinkingMode)
            {
                _dragHandler.CancelDrag();
            }
        }

        private void OnViewDragging(VertexView view, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            _viewMover.TryMoveView(view, ref pos);                                        
        }

        private void OnViewDragEnded(VertexView view, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            _viewMover.TryMoveView(view, ref pos);
        }        

        private void OnViewSelected(VertexView view, bool b)
        {
            view.ShowSelectedMarker(b);
        }

        private void OnViewMoved(VertexView view, Vector2 pos)
        {
            UpdateNodePosition(view, pos);
            RedrawLinkViews(view);
        }

        private void OnNodeBlocked(VertexData node, bool b)
        {
            var view = _nodeViews.GetItem(node.Id);
            view?.ShowBlockedMarker(b);

            OnFieldChanged();
        }

        private void OnFieldChanged()
        {
            OnPathChanged(_pathSetter.IsReady);
        }

        private void OnStartNodeChanged(VertexData node, bool b)
        {
            var view = _nodeViews.GetItem(node.Id);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(VertexData node, bool b)
        {
            var view = _nodeViews.GetItem(node.Id);
            view?.ShowFinishMarker(b);
        }

        private void OnPathChanged(bool isReady)
        {
            _pathDrawer.ShowPath(false);
            TryRun(isReady);
        }

        private readonly List<VertexView> _viewsPath = new List<VertexView>();

        private void TryRun(bool isReady)
        {
            if (isReady)
            {
                var nodesPath = _pathFinder.GetPath(_pathSetter.StartNode, _pathSetter.FinishNode);
                if (nodesPath != null)
                {
                    _nodeViews.NodesToViewsNonAlloc(nodesPath, _viewsPath);
                    _pathDrawer.SetPath(_viewsPath);
                    _pathDrawer.ShowPath(true);
                }
            }
        }

        private Task _saveloadTask;

        private async void OnSaveBtnClicked()
        {
            if (_saveloadTask != null && !_saveloadTask.IsCompleted)
            {
                return;
            }

            try
            {
                var saveDto = _dtoProvider.GetDto();
                
                _saveloadTask = _saver.SaveAsync<VertexesFieldSaveDto>(saveDto);
                await _saveloadTask;
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                _saveloadTask = null;
            }
        }

        private async void OnLoadBtnClicked()
        {
            if (_saveloadTask != null && !_saveloadTask.IsCompleted)
            {
                return;
            }

            try
            {
                var loadTask = _loader.LoadAsync<VertexesFieldSaveDto>();
                _saveloadTask = loadTask;

                var dto = await loadTask;
                _pathSetter.UpdateStartNode(_pathSetter.StartNode);
                _pathSetter.UpdateFinishNode(_pathSetter.FinishNode);
                _builder.BuildFromDto(dto);                
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            finally
            {
                _saveloadTask = null;
            }
        }
    }
}