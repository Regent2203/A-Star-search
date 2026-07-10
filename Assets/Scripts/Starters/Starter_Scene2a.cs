using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ThisProject.Implementations.Vertexes;
using ThisProject.Inputs;
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
        private VertexesClickHandler _clickHandler;
        private VertexesDragHandler _dragHandler;
        private VertexesFieldBuilder _builder;
        private NodeBlocker<VertexData> _nodeBlocker;
        private NodeViewSelector<VertexView> _viewSelector;
        private NodeViewMover _viewMover;
        private VertexesVisualLinksCreator _visualLinksCreator;
        private PathSetter<VertexData> _pathSetter;
        private PathFinder<VertexData> _pathFinder;
        private LinePathDrawer _pathDrawer;
        private ISaver _saver;
        private ILoader<FieldSaveDto<VertexDataDto, int>> _loader;
        //private UIVertexesHotkeyInfoPanel _hotkeyInfoPanel;
        private UISaveLoadPanel _saveLoadPanel;


        [Inject]
        public void Construct(VertexDataStorage nodes, VertexViewStorage views,
            VertexesClickHandler clickHandler, VertexesDragHandler dragHandler, VertexesFieldBuilder builder,
            NodeBlocker<VertexData> nodeBlocker, NodeViewSelector<VertexView> viewSelector, NodeViewMover viewMover, 
            VertexesVisualLinksCreator visualLinksCreator,
            PathSetter<VertexData> pathSetter, PathFinder<VertexData> pathFinder, LinePathDrawer pathDrawer,
            ISaver saver, ILoader<FieldSaveDto<VertexDataDto, int>> loader,
            UISaveLoadPanel saveLoadPanel)
        {
            _nodes = nodes;
            _views = views;
            _clickHandler = clickHandler;
            _dragHandler = dragHandler;
            _builder = builder;
            _nodeBlocker = nodeBlocker;
            _viewSelector = viewSelector;
            _viewMover = viewMover;

            _visualLinksCreator = visualLinksCreator;

            _pathSetter = pathSetter;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;

            _saver = saver;
            _loader = loader;

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
            _builder.TestPopulate(6);
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
                    switch (button)
                    {/*
                        case PointerEventData.InputButton.Left:
                            _visualLinksCreator.TryCreateLink(_viewSelector.SelectedView, node);
                            break;
                        case PointerEventData.InputButton.Right:
                            _visualLinksCreator.TryDeleteLink(_viewSelector.SelectedView, node);
                            break;*/
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
            if (_viewMover.TryMoveView(view, ref pos))
                UpdateNodePosition(view, pos);
        }

        private void OnViewDragEnded(VertexView view, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            if (_viewMover.TryMoveView(view, ref pos))
                UpdateNodePosition(view, pos);
        }

        private void OnViewSelected(VertexView view, bool b)
        {
            view.ShowSelectedMarker(b);
        }

        private void OnViewMoved(INodeView view, Vector2 vector)
        {
            //todo visual link update; also fieldchanged?
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
                _saveloadTask = _saver.SaveAsync();
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
                var loadTask = _loader.LoadAsync();
                _saveloadTask = loadTask;

                var dto = await loadTask;
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