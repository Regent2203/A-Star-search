using System;
using System.Collections.Generic;
using ThisProject.Fields.NodeBlockers;
using ThisProject.Fields.ViewMovers;
using ThisProject.Fields.ViewSelectors;
using ThisProject.Implementations.Cells;
using ThisProject.Implementations.Vertexes;
using ThisProject.Inputs;
using ThisProject.ObjectsStorages;
using ThisProject.PathDrawers;
using ThisProject.PathFinders;
using ThisProject.PathSetters;
using ThisProject.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Starters
{
    public class Starter_Scene2a : StarterBase
    {
        private VertexesField _field;
        private DictTypeStorage<VertexNode, int> _nodes;
        private DictTypeStorage<VertexView, int> _views;
        private VertexesClickHandler _clickHandler;
        private VertexesDragHandler _dragHandler;
        private VertexesFieldGenerator _generator;
        private NodeBlocker<VertexNode> _nodeBlocker;
        private ViewSelector<VertexView> _viewSelector;
        private ViewMover _viewMover;
        private VertexesVisualLinksCreator _visualLinksCreator;
        private PathSetter<VertexNode> _pathSetter;
        private PathFinder<VertexNode> _pathFinder;
        private LinePathDrawer _pathDrawer;
        //private UIVertexesHotkeyInfoPanel _hotkeyInfoPanel;


        [Inject]
        public void Construct(VertexesField field, DictTypeStorage<VertexNode, int> nodes, DictTypeStorage<VertexView, int> views,
            VertexesClickHandler clickHandler, VertexesDragHandler dragHandler, VertexesFieldGenerator generator,
            NodeBlocker<VertexNode> nodeBlocker, ViewSelector<VertexView> viewSelector, ViewMover viewMover, 
            VertexesVisualLinksCreator visualLinksCreator,
            PathSetter<VertexNode> pathSetter, PathFinder<VertexNode> pathFinder, LinePathDrawer pathDrawer)
        {
            _field = field;
            _nodes = nodes;
            _views = views;
            _clickHandler = clickHandler;
            _dragHandler = dragHandler;
            _generator = generator;
            _nodeBlocker = nodeBlocker;
            _viewSelector = viewSelector;
            _viewMover = viewMover;

            _visualLinksCreator = visualLinksCreator;

            _pathSetter = pathSetter;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
        }

        protected override void SubscribeAll()
        {
            _clickHandler.ViewClicked += OnViewClicked;
            _clickHandler.FieldClicked += OnFieldClicked;
            _dragHandler.ViewDragStarted += OnViewDragStarted;
            _dragHandler.ViewDragging += OnViewDragging;
            _dragHandler.ViewDragEnded += OnViewDragEnded;

            _viewSelector.ViewSelected += OnViewSelected;
            _viewMover.ViewMoved += OnViewMoved;
            _nodeBlocker.NodeBlocked += OnNodeBlocked;

            _pathSetter.StartNodeChanged += OnStartNodeChanged;
            _pathSetter.FinishNodeChanged += OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged += OnPathChanged;
        }

        protected override void InitDefaultStates()
        {
            //todo
            _generator.TestPopulate(6);
        }

        protected override void UnsubscribeAll()
        {
            _clickHandler.ViewClicked -= OnViewClicked;
            _clickHandler.FieldClicked -= OnFieldClicked;
            _dragHandler.ViewDragStarted -= OnViewDragStarted;
            _dragHandler.ViewDragging -= OnViewDragging;
            _dragHandler.ViewDragEnded -= OnViewDragEnded;

            _viewSelector.ViewSelected -= OnViewSelected;
            _viewMover.ViewMoved -= OnViewMoved;
            _nodeBlocker.NodeBlocked -= OnNodeBlocked;

            _pathSetter.StartNodeChanged -= OnStartNodeChanged;
            _pathSetter.FinishNodeChanged -= OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged -= OnPathChanged;
        }

        private void UpdateNodePosition(VertexView view, Vector2 pos)
        {
            var node = _nodes.GetItemById(view.Id);
            if (node.TryChangeNodePosition(pos))
                OnFieldChanged();
        }

        private void OnViewClicked(VertexView view, PointerEventData.InputButton button, InputSnapshot input)
        {
            var node = _nodes.GetItemById(view.Id);

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
            if (_viewMover.TryMoveView(view, pos))
                UpdateNodePosition(view, pos);
        }

        private void OnViewDragEnded(VertexView view, Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            if (_viewMover.TryMoveView(view, pos))
                UpdateNodePosition(view, pos);
        }

        private void OnViewSelected(VertexView view, bool b)
        {
            view.ShowSelectedMarker(b);
        }

        private void OnViewMoved(IView view, Vector2 vector)
        {
            //todo visual link update; also fieldchanged?
        }

        private void OnNodeBlocked(VertexNode node, bool b)
        {
            var view = _views.GetItemById(node.Id);
            view?.ShowBlockedMarker(b);

            OnFieldChanged();
        }

        private void OnFieldChanged()
        {
            OnPathChanged(_pathSetter.IsReady);
        }

        private void OnStartNodeChanged(VertexNode node, bool b)
        {
            var view = _views.GetItemById(node.Id);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(VertexNode node, bool b)
        {
            var view = _views.GetItemById(node.Id);
            view?.ShowFinishMarker(b);
        }

        private void OnPathChanged(bool isReady)
        {
            _pathDrawer.ShowPath(false);
            TryRun(isReady);
        }

        private List<VertexView> _viewsPath = new List<VertexView>();

        private void TryRun(bool isReady)
        {
            if (isReady)
            {
                var nodesPath = _pathFinder.GetPath(_pathSetter.StartNode, _pathSetter.FinishNode);
                if (nodesPath != null)
                {
                    //_field.NodesToViewsNonAlloc(nodesPath, _viewsPath); //todo
                    _pathDrawer.SetPath(_viewsPath);
                    _pathDrawer.ShowPath(true);
                }
            }
        }
    }
}