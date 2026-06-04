using System.Collections.Generic;
using ThisProject.Fields.ViewMovers;
using ThisProject.Implementations.Vertexes;
using ThisProject.Implementations.Vertexes.UI;
using ThisProject.Inputs;
using ThisProject.PathDrawers;
using ThisProject.PathFinders;
using ThisProject.PathSetters;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Windows;
using Zenject;

namespace ThisProject.Starters
{
    public class Starter_Scene2a : StarterBase
    {
        private VertexesField _field;
        private VertexesFieldGenerator _generator;
        private VertexesClickHandler _clickHandler;
        private VertexesDragHandler _dragHandler;
        private SpatialViewMover _viewMover;
        private VertexesVisualLinksCreator _visualLinksCreator;
        private PathSetter<VertexNode> _pathSetter;
        private PathFinder<VertexNode> _pathFinder;
        private LinePathDrawer _pathDrawer;
        //private UIVertexesHotkeyInfoPanel _hotkeyInfoPanel;


        [Inject]
        public void Construct(VertexesField field, VertexesFieldGenerator generator,
            VertexesClickHandler clickHandler, VertexesDragHandler dragHandler,
            SpatialViewMover viewMover,
            VertexesVisualLinksCreator visualLinksCreator,
            PathSetter<VertexNode> pathSetter, PathFinder<VertexNode> pathFinder, LinePathDrawer pathDrawer)
        {
            _field = field;
            _generator = generator;
            _clickHandler = clickHandler;
            _dragHandler = dragHandler;
            _viewMover = viewMover;

            _visualLinksCreator = visualLinksCreator;

            _pathSetter = pathSetter;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
        }

        protected override void SubscribeAll()
        {
            _clickHandler.ViewClicked += OnViewClicked;
            _dragHandler.ViewDragStarted += OnViewDragStarted;
            _dragHandler.ViewDragging += OnViewDragging;
            _dragHandler.ViewDragEnded += OnViewDragEnded;

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
            //
        }

        private void OLD_OnNodeClicked(VertexNode node, PointerEventData.InputButton button, InputSnapshot input)
        {
            /*
            

            if (input.IsLinkingMode)
            {
                _visualLinksCreator.TryUseNode(node, button);
            }
            */
        }

        private void OnViewDragStarted(VertexView view, Vector2 pos, PointerEventData data)
        {
            //
        }

        private void OnViewDragging(VertexView view, Vector2 pos, PointerEventData data)
        {
            _viewMover.TryMoveView(view, pos);
        }

        private void OnViewDragEnded(VertexView view, Vector2 pos, Vector2 oldPos, PointerEventData data)
        {
            //_viewMover.TryMoveView(view, pos);
            //
        }

        private void OnViewClicked(VertexView view, PointerEventData.InputButton button, InputSnapshot input)
        {
            var node = _field.GetNodeById(view.Id);

            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                if (button == PointerEventData.InputButton.Right)
                    node.TrySetBlocked(!node.IsBlocked);
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
        }

        private void OnFieldChanged()
        {
            OnPathChanged(_pathSetter.IsReady);
        }

        private void OnStartNodeChanged(VertexNode node, bool b)
        {
            var view = _field.GetViewById(node.Id);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(VertexNode node, bool b)
        {
            var view = _field.GetViewById(node.Id);
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
                    _field.NodesToViewsNonAlloc(nodesPath, _viewsPath);
                    _pathDrawer.SetPath(_viewsPath);
                    _pathDrawer.ShowPath(true);
                }
            }
        }
    }
}