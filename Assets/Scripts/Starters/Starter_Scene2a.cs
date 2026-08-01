using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThisProject.Implementations.Vertexes;
using ThisProject.Inputs;
using ThisProject.Links;
using ThisProject.Links.Implementations;
using ThisProject.Links.Providers;
using ThisProject.Nodes;
using ThisProject.Nodes.NodeBlockers;
using ThisProject.Nodes.ViewMovers;
using ThisProject.Nodes.ViewSelectors;
using ThisProject.PathDrawers;
using ThisProject.PathFinders;
using ThisProject.PathSetters;
using ThisProject.SaveSystem;
using ThisProject.SaveSystem.Dto;
using ThisProject.UICommon;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Starters
{
    public class Starter_Scene2a : StarterBase
    {
        private VertexDataStorage _nodes;
        private VertexViewStorage _views;
        private LinkViewStorage_Int _linkViews;
        private VertexesClickHandler _clickHandler;
        private VertexesDragHandler _dragHandler;
        private VertexesFieldBuilder _builder;
        private NodeBlocker<VertexData> _nodeBlocker;
        private NodeViewSelector<VertexView> _viewSelector;
        private NodeViewMover<VertexView> _viewMover;
        private VertexesLinksBuilder _linksBuilder;
        private StoredLinksProvider<VertexData, LinkData<int>, int> _linksProvider;
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
            NodeBlocker<VertexData> nodeBlocker, NodeViewSelector<VertexView> viewSelector, NodeViewMover<VertexView> viewMover, 
            VertexesLinksBuilder linksBuilder, StoredLinksProvider<VertexData, LinkData<int>, int> linksProvider,
            PathSetter<VertexData> pathSetter, PathFinder<VertexData, int> pathFinder, LinePathDrawer pathDrawer,
            ISaver saver, ILoader loader, VertexesFieldSaveDtoProvider dtoProvider,
            UISaveLoadPanel saveLoadPanel)
        {
            _nodes = nodes;
            _views = views;
            _linkViews = linkViews;
            _clickHandler = clickHandler;
            _dragHandler = dragHandler;
            _builder = builder;
            _nodeBlocker = nodeBlocker;
            _viewSelector = viewSelector;
            _viewMover = viewMover;

            _linksBuilder = linksBuilder;
            _linksProvider = linksProvider;

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
            var node = _nodes.GetItem(view.Id);
            if (node.TryChangeNodePosition(pos))
                OnFieldChanged();
        }

        //todo move?
        private void RedrawLinkViews(VertexView view)
        {
            ILinkView linkView;
            var fromLinks = _linksProvider.GetLinksFromNode(_nodes.GetItem(view.Id));
            var toLinks = _linksProvider.GetLinksToNode(_nodes.GetItem(view.Id));

            foreach (var linkData in fromLinks)
            {
                linkView = _linkViews.GetItem(new LinkKey<int>(view.Id, linkData.Id.To));
                linkView.UpdatePositions();
            }
            foreach (var linkData in toLinks)
            {
                linkView = _linkViews.GetItem(new LinkKey<int>(linkData.Id.From, view.Id));
                linkView.UpdatePositions();
            }
        }

        private void OnViewClicked(VertexView view, PointerEventData.InputButton button, InputSnapshot input)
        {
            var node = _nodes.GetItem(view.Id);

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
                    var selectedNode = _nodes.GetItem(_viewSelector.SelectedView.Id);

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
        }

        private void OnFieldClicked(PointerEventData.InputButton button, InputSnapshot snapshot)
        {
            if (button == PointerEventData.InputButton.Left)
                _viewSelector.SelectView(null);
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
            var view = _views.GetItem(node.Id);
            view?.ShowBlockedMarker(b);

            OnFieldChanged();
        }

        private void OnFieldChanged()
        {
            OnPathChanged(_pathSetter.IsReady);
        }

        private void OnStartNodeChanged(VertexData node, bool b)
        {
            var view = _views.GetItem(node.Id);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(VertexData node, bool b)
        {
            var view = _views.GetItem(node.Id);
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
                    _views.NodesToViewsNonAlloc(nodesPath, _viewsPath);
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