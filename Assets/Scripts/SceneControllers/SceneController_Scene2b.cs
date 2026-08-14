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
using EasyField.Nodes.NodePositionChanger;
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
    public class SceneController_Scene2b : SceneControllerBase
    {
        private VertexesFieldBuilder _fieldBuilder;
        private VertexDataStorage _nodeDatas;
        private VertexViewStorage _nodeViews;
        private LinkViewStorage_Int _linkViews;
        private VertexesLinksClickHandler _clickHandler;
        private VertexesDragHandler _dragHandler;
        private LinkCostAdder<LinkData<int>> _linkCostSetter;
        private NodePositionChanger<VertexData> _nodePositionChanger;
        private NodeBlocker<VertexData> _nodeBlocker;
        private NodeViewSelector<VertexView> _nodeViewSelector;
        private NodeViewMover<VertexView> _nodeViewMover;
        private StoredLinksProvider<LinkData<int>, int> _linksProvider;
        private LinkViewCoordinator<VertexView, int> _linkViewCoordinator;
        private PathSetter<VertexData> _pathSetter;
        private PathFinder<VertexData, int> _pathFinder;
        private LinePathDrawer<VertexView> _pathDrawer;
        private VertexesSaveLoadManager _saveLoadManager;
        private UIMainButtonsPanel _saveLoadPanel;


        [Inject]
        public void Construct(VertexesFieldBuilder fieldBuilder, VertexDataStorage nodeDatas, VertexViewStorage nodeViews, LinkViewStorage_Int linkViews,
            VertexesLinksClickHandler clickHandler, VertexesDragHandler dragHandler,
            LinkCostAdder<LinkData<int>> linkCostSetter,
            NodeBlocker<VertexData> nodeBlocker, NodePositionChanger<VertexData> nodePositionChanger,
            NodeViewSelector<VertexView> nodeViewSelector, NodeViewMover<VertexView> nodeViewMover, 
            StoredLinksProvider<LinkData<int>, int> linksProvider, LinkViewCoordinator<VertexView, int> linkViewCoordinator,
            PathSetter<VertexData> pathSetter, PathFinder<VertexData, int> pathFinder, LinePathDrawer<VertexView> pathDrawer,
            VertexesSaveLoadManager saveLoadManager, UIMainButtonsPanel saveLoadPanel)
        {
            _fieldBuilder = fieldBuilder;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;
            _linkViews = linkViews;

            _clickHandler = clickHandler;
            _dragHandler = dragHandler;
            
            _linkCostSetter = linkCostSetter;

            _nodePositionChanger = nodePositionChanger;
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
            _clickHandler.LinkViewClicked += OnLinkViewClicked;
            _clickHandler.NodeViewClicked += OnNodeViewClicked;
            _clickHandler.FieldClicked += OnFieldClicked;
            _dragHandler.NodeViewDragStarted += OnNodeViewDragStarted;
            _dragHandler.NodeViewDragging += OnNodeViewDragging;
            _dragHandler.NodeViewDragEnded += OnNodeViewDragEnded;

            _nodeViewSelector.NodeViewSelected += OnNodeViewSelected;
            _nodeViewMover.NodeViewMoved += OnNodeViewMoved;
            _linkCostSetter.LinkCostChanged += OnLinkCostChanged;
            _nodePositionChanger.NodePositionChanged += OnNodePositionChanged;
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
            _clickHandler.LinkViewClicked -= OnLinkViewClicked;
            _clickHandler.NodeViewClicked -= OnNodeViewClicked;
            _clickHandler.FieldClicked -= OnFieldClicked;
            _dragHandler.NodeViewDragStarted -= OnNodeViewDragStarted;
            _dragHandler.NodeViewDragging -= OnNodeViewDragging;
            _dragHandler.NodeViewDragEnded -= OnNodeViewDragEnded;

            _nodeViewSelector.NodeViewSelected -= OnNodeViewSelected;
            _nodeViewMover.NodeViewMoved -= OnNodeViewMoved;
            _linkCostSetter.LinkCostChanged -= OnLinkCostChanged;
            _nodePositionChanger.NodePositionChanged -= OnNodePositionChanged;
            _nodeBlocker.NodeBlocked -= OnNodeBlocked;

            _pathSetter.StartNodeChanged -= OnStartNodeChanged;
            _pathSetter.FinishNodeChanged -= OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged -= OnPathChanged;

            _saveLoadPanel.SaveBtnClicked -= OnSaveBtnClicked;
            _saveLoadPanel.LoadBtnClicked -= OnLoadBtnClicked;
            _saveLoadPanel.NewBtnClicked -= OnNewBtnClicked;
        }

        private void OnLinkViewClicked(LinkView_Int view, PointerEventData.InputButton button, InputSnapshot input)
        {
            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                if (_linksProvider.TryGetLink(view.From, view.To, out var linkData))
                {
                    const float value = 0.5f;

                    if (button == PointerEventData.InputButton.Left)
                        _linkCostSetter.ChangeLinkCost(linkData, value);

                    if (button == PointerEventData.InputButton.Right)
                        _linkCostSetter.ChangeLinkCost(linkData, -value);
                }
            }
        }

        private void OnNodeViewClicked(VertexView nodeView, PointerEventData.InputButton button, InputSnapshot input)
        {
            var nodeData = _nodeDatas.GetItem(nodeView.Id);

            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                if (button == PointerEventData.InputButton.Left)
                    _nodeViewSelector.SelectView(nodeView);

                if (button == PointerEventData.InputButton.Right)
                    _nodeBlocker.TryBlockNode(nodeData, !nodeData.IsBlocked);
            }

            if (input.IsMarkingMode)
            {
                switch (button)
                {
                    case PointerEventData.InputButton.Left:
                        _pathSetter.UpdateStartNode(nodeData);
                        break;
                    case PointerEventData.InputButton.Right:
                        _pathSetter.UpdateFinishNode(nodeData);
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
                            if (_fieldBuilder.TryCreateLink(selectedNode, nodeData))
                                OnFieldChanged();
                            break;
                        case PointerEventData.InputButton.Right:
                            if (_fieldBuilder.TryDeleteLink(selectedNode, nodeData))
                                OnFieldChanged();
                            break;
                    }                    
                }
            }

            if (input.IsCreatingMode)
            {
                if (button == PointerEventData.InputButton.Right)
                    _fieldBuilder.DeleteNode(nodeView.Id);
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

        private void OnNodeViewDragStarted(VertexView nodeView, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            if ((button != PointerEventData.InputButton.Left) || 
                input.IsMarkingMode || input.IsCreatingMode || input.IsLinkingMode)
            {
                _dragHandler.CancelDrag();
            }
        }

        private void OnNodeViewDragging(VertexView nodeView, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            _nodeViewMover.TryMoveView(nodeView, ref pos);                                        
        }

        private void OnNodeViewDragEnded(VertexView nodeView, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            _nodeViewMover.TryMoveView(nodeView, ref pos);
        }        

        private void OnNodeViewSelected(VertexView nodeView, bool b)
        {
            nodeView.ShowSelectedMarker(b);
        }

        private void OnNodeViewMoved(VertexView nodeView, Vector2 pos)
        {
            var nodeData = _nodeDatas.GetItem(nodeView.Id);
            _nodePositionChanger.TryChangeNodePosition(nodeData, pos);            
        }

        private void OnLinkCostChanged(LinkData<int> linkData, float cost)
        {
            var linkView = _linkViews.GetItem(linkData.Id);
            linkView.UpdateCostText(cost);

            OnFieldChanged();
        }

        private void OnNodePositionChanged(VertexData nodeData, Vector2 pos)
        {
            UpdateLinksAroundNode(nodeData.Id);
            OnFieldChanged();
        }

        private void OnNodeBlocked(VertexData nodeData, bool b)
        {
            var nodeView = _nodeViews.GetItem(nodeData.Id);
            nodeView?.ShowBlockedMarker(b);

            OnFieldChanged();
        }

        private void UpdateLinksAroundNode(int id)
        {
            LinkView<int> linkView;
            var fromLinks = _linksProvider.GetLinksFromNode(id);
            var toLinks = _linksProvider.GetLinksToNode(id);

            foreach (var linkData in fromLinks)
            {
                linkView = _linkViews.GetItem(new DualKey<int>(id, linkData.Id.To));
                _linkViewCoordinator.CheckSingle(linkView);
            }
            foreach (var linkData in toLinks)
            {
                linkView = _linkViews.GetItem(new DualKey<int>(linkData.Id.From, id));
                _linkViewCoordinator.CheckSingle(linkView);
            }
        }

        private void OnFieldChanged()
        {
            OnPathChanged(_pathSetter.IsReady);
        }

        private void OnStartNodeChanged(VertexData nodeData, bool b)
        {
            if (nodeData != null)
            {
                var nodeView = _nodeViews.GetItem(nodeData.Id);
                nodeView?.ShowStartMarker(b);
            }
        }

        private void OnFinishNodeChanged(VertexData nodeData, bool b)
        {
            if (nodeData != null)
            {
                var nodeView = _nodeViews.GetItem(nodeData.Id);
                nodeView?.ShowFinishMarker(b);
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
            sizeX = Mathf.Max(sizeX, 1);
            sizeY = Mathf.Max(sizeY, 1);
            _fieldBuilder.CreateNewField(sizeX, sizeY);
        }
    }
}