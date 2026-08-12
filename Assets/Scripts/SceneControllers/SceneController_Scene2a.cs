using EasyField.Implementations.Vertexes;
using EasyField.Implementations.Vertexes.Core.Dto;
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
using EasyField.UICommon;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.SceneControllers
{
    public class SceneController_Scene2a : SceneControllerBase
    {
        private VertexesFieldBuilder _fieldBuilder;
        private VertexDataStorage _nodeDatas;
        private VertexViewStorage _nodeViews;
        private LinkViewStorage_Int _linkViews;
        private VertexesClickHandler _clickHandler;
        private VertexesDragHandler _dragHandler;        
        private ICostProvider<VertexData> _costProvider;
        private LinkCostSetter<LinkData<int>> _linkCostSetter;
        private NodeBlocker<VertexData> _nodeBlocker;
        private NodeViewSelector<VertexView> _nodeViewSelector;
        private NodeViewMover<VertexView> _nodeViewMover;
        private StoredLinksProvider<LinkData<int>, int> _linksProvider;
        private LinkViewCoordinator<VertexView, int> _linkViewCoordinator;
        private PathSetter<VertexData> _pathSetter;
        private PathFinder<VertexData, int> _pathFinder;
        private IPathDrawer<VertexView> _pathDrawer;
        private VertexesSaveLoadManager _saveLoadManager;
        private UIMainButtonsPanel _saveLoadPanel;


        [Inject]
        public void Construct(VertexesFieldBuilder fieldBuilder, VertexDataStorage nodeDatas, VertexViewStorage nodeViews, LinkViewStorage_Int linkViews,
            VertexesClickHandler clickHandler, VertexesDragHandler dragHandler, 
            ICostProvider<VertexData> costProvider, LinkCostSetter<LinkData<int>> linkCostSetter,
            NodeBlocker<VertexData> nodeBlocker, NodeViewSelector<VertexView> nodeViewSelector, NodeViewMover<VertexView> nodeViewMover, 
            StoredLinksProvider<LinkData<int>, int> linksProvider, LinkViewCoordinator<VertexView, int> linkViewCoordinator,
            PathSetter<VertexData> pathSetter, PathFinder<VertexData, int> pathFinder, IPathDrawer<VertexView> pathDrawer,
            VertexesSaveLoadManager saveLoadManager, UIMainButtonsPanel saveLoadPanel)
        {
            _fieldBuilder = fieldBuilder;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;
            _linkViews = linkViews;

            _clickHandler = clickHandler;
            _dragHandler = dragHandler;
            
            _costProvider = costProvider;
            _linkCostSetter = linkCostSetter;

            _nodeBlocker = nodeBlocker;
            _nodeViewSelector = nodeViewSelector;
            _nodeViewMover = nodeViewMover;

            _linksProvider = linksProvider;
            _linkViewCoordinator = linkViewCoordinator;

            _pathSetter = pathSetter;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;

            _saveLoadManager = saveLoadManager;
            _saveLoadPanel = saveLoadPanel;
        }

        protected override void SubscribeAll()
        {
            _clickHandler.NodeViewClicked += OnNodeViewClicked;
            _clickHandler.FieldClicked += OnFieldClicked;
            _dragHandler.NodeViewDragStarted += OnNodeViewDragStarted;
            _dragHandler.NodeViewDragging += OnNodeViewDragging;
            _dragHandler.NodeViewDragEnded += OnNodeViewDragEnded;

            _nodeViewSelector.NodeViewSelected += OnNodeViewSelected;
            _nodeViewMover.NodeViewMoved += OnNodeViewMoved;
            _nodeBlocker.NodeBlocked += OnNodeBlocked;

            _pathSetter.StartNodeChanged += OnStartNodeChanged;
            _pathSetter.FinishNodeChanged += OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged += OnPathChanged;

            _saveLoadPanel.SaveBtnClicked += OnSaveBtnClicked;
            _saveLoadPanel.LoadBtnClicked += OnLoadBtnClicked;
            _saveLoadPanel.NewBtnClicked += OnNewBtnClicked;
        }

        protected override void InitDefaultStates()
        {
            _fieldBuilder.CreateNewField(64, 40);
        }

        protected override void UnsubscribeAll()
        {
            _clickHandler.NodeViewClicked -= OnNodeViewClicked;
            _clickHandler.FieldClicked -= OnFieldClicked;
            _dragHandler.NodeViewDragStarted -= OnNodeViewDragStarted;
            _dragHandler.NodeViewDragging -= OnNodeViewDragging;
            _dragHandler.NodeViewDragEnded -= OnNodeViewDragEnded;

            _nodeViewSelector.NodeViewSelected -= OnNodeViewSelected;
            _nodeViewMover.NodeViewMoved -= OnNodeViewMoved;
            _nodeBlocker.NodeBlocked -= OnNodeBlocked;

            _pathSetter.StartNodeChanged -= OnStartNodeChanged;
            _pathSetter.FinishNodeChanged -= OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged -= OnPathChanged;

            _saveLoadPanel.SaveBtnClicked -= OnSaveBtnClicked;
            _saveLoadPanel.LoadBtnClicked -= OnLoadBtnClicked;
            _saveLoadPanel.NewBtnClicked -= OnNewBtnClicked;
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
                linkView = _linkViews.GetItem(new DualKey<int>(view.Id, linkData.Id.To));
                _linkViewCoordinator.CheckSingle(linkView);

                //todo
                var from = _nodeDatas.GetItem(linkData.From);
                var to = _nodeDatas.GetItem(linkData.To);
                cost = _costProvider.GetCost(from, to);
                _linkCostSetter.SetLinkCost(linkData, cost);
                linkView.UpdateCostText(cost); //todo onlinkcostchanged
            }
            foreach (var linkData in toLinks)
            {
                linkView = _linkViews.GetItem(new DualKey<int>(linkData.Id.From, view.Id));
                _linkViewCoordinator.CheckSingle(linkView);

                //todo
                var from = _nodeDatas.GetItem(linkData.From);
                var to = _nodeDatas.GetItem(linkData.To);
                cost = _costProvider.GetCost(from, to);
                _linkCostSetter.SetLinkCost(linkData, cost);
                linkView.UpdateCostText(cost);
            }
        }

        private void OnNodeViewClicked(VertexView view, PointerEventData.InputButton button, InputSnapshot input)
        {
            var node = _nodeDatas.GetItem(view.Id);

            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                if (button == PointerEventData.InputButton.Left)
                    _nodeViewSelector.SelectView(view);

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
                if (_nodeViewSelector.SelectedNodeView != null)
                {
                    var selectedNode = _nodeDatas.GetItem(_nodeViewSelector.SelectedNodeView.Id);

                    switch (button)
                    {
                        case PointerEventData.InputButton.Left:
                            if (_fieldBuilder.TryCreateLink(selectedNode, node))
                                OnFieldChanged();
                            break;
                        case PointerEventData.InputButton.Right:
                            if (_fieldBuilder.TryDeleteLink(selectedNode, node))
                                OnFieldChanged();
                            break;
                    }                    
                }
            }

            if (input.IsCreatingMode)
            {
                if (button == PointerEventData.InputButton.Right)
                    _fieldBuilder.DeleteNode(view.Id);
            }
        }

        private void OnFieldClicked(Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            if (button == PointerEventData.InputButton.Left)
            {
                if (input.IsCreatingMode)
                    _fieldBuilder.CreateNode(pos);
                else
                    _nodeViewSelector.SelectView(null);
            }   
        }

        private void OnNodeViewDragStarted(VertexView view, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            if ((button != PointerEventData.InputButton.Left) || 
                input.IsMarkingMode || input.IsCreatingMode || input.IsLinkingMode)
            {
                _dragHandler.CancelDrag();
            }
        }

        private void OnNodeViewDragging(VertexView view, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            _nodeViewMover.TryMoveView(view, ref pos);                                        
        }

        private void OnNodeViewDragEnded(VertexView view, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            _nodeViewMover.TryMoveView(view, ref pos);
        }        

        private void OnNodeViewSelected(VertexView view, bool b)
        {
            view.ShowSelectedMarker(b);
        }

        private void OnNodeViewMoved(VertexView view, Vector2 pos)
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
            if (node != null)
            {
                var view = _nodeViews.GetItem(node.Id);
                view?.ShowStartMarker(b);
            }
        }

        private void OnFinishNodeChanged(VertexData node, bool b)
        {
            if (node != null)
            {
                var view = _nodeViews.GetItem(node.Id);
                view?.ShowFinishMarker(b);
            }
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

        private void OnSaveBtnClicked()
        {
            _saveLoadManager.StartSaving();
        }

        private async void OnLoadBtnClicked()
        {
            var dto = await _saveLoadManager.StartLoading();
            _fieldBuilder.BuildFromDto(dto);
        }

        private void OnNewBtnClicked(int sizeX, int sizeY)
        {
            _fieldBuilder.CreateNewField(sizeX, sizeY);
        }
    }
}