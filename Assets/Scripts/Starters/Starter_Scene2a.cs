using System;
using System.Collections.Generic;
using ThisProject.Implementations.Cells;
using ThisProject.Implementations.Vertexes;
using ThisProject.Inputs;
using ThisProject.PathDrawers;
using ThisProject.PathFinders;
using ThisProject.PathSetters;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Starters
{
    public class Starter_Scene2a : StarterBase
    {
        //private CellsConfig _config;
        private VertexesField _field;
        //private GridClickHandler<CellNode> _fieldInputHandler;
        private VertexesVisualLinksCreator _visualLinksCreator;
        private PathSetter<VertexNode> _pathSetter;
        private PathFinder<VertexNode> _pathFinder;
        private LinePathDrawer _pathDrawer;
        //private UIHotkeyInfoPanel_Vertexes _hotkeyInfoPanel;


        [Inject]
        /*
        public void Construct(CellsConfig config, CellsField field, GridClickHandler<CellNode> fieldInputHandler,
            palette, UICellsPaletteChoicePanel paletteChoice, UICellsPaletteHotkeyInfoPanel hotkeyInfoPanel)*/
        public void Construct(VertexesField field,VertexesVisualLinksCreator visualLinksCreator,
            PathSetter<VertexNode> pathSetter, PathFinder<VertexNode> pathFinder, LinePathDrawer pathDrawer)
        {
            _field = field;
            _visualLinksCreator = visualLinksCreator;

            _pathSetter = pathSetter;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            /*
            _config = config;
            _fieldInputHandler = fieldInputHandler;
            _hotkeyInfoPanel = hotkeyInfoPanel;*/
        }

        protected override void SubscribeAll()
        {
            //_field.NodeClicked += OnNodeClicked;
            //_views.FieldChanged += OnFieldChanged;

            _pathSetter.StartNodeChanged += OnStartNodeChanged;
            _pathSetter.FinishNodeChanged += OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged += ClearPath;
            _pathSetter.AnyNodeChanged += TryRun;

            //NodeMoved -> _pathDrawer.ShowPath(false); TryRun(_pathSetter.IsReady);
            //NodeBlockStateChanged -> _pathDrawer.ShowPath(false); TryRun(_pathSetter.IsReady);            

            _pathSetter.AnyNodeChanged += (b) => _pathDrawer.ShowPath(false);
            _pathSetter.StartNodeChanged += (node, b) =>
            {
                var view = _field.GetViewById(node.Id);
                view?.ShowStartMarker(b);
            };
            _pathSetter.FinishNodeChanged += (node, b) =>
            {
                var view = _field.GetViewById(node.Id);
                view?.ShowFinishMarker(b);
            };

            _pathSetter.AnyNodeChanged += TryRun;

            //NodeClicked += -> LinksCreator.TryUseFirstNode
            //mouseScroll -> ChangeLinkCost

            //temp
            //_visualLinksCreator.TryUseNode(_views.Node1);
            //_visualLinksCreator.TryUseNode(_views.Node2);
        }

        protected override void InitDefaultStates()
        {
            //
        }

        protected override void UnsubscribeAll()
        {
            //
        }

        private void OnNodeClicked(VertexNode node, PointerEventData.InputButton button, InputSnapshot input)
        {
            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                if (button == PointerEventData.InputButton.Right)
                    node.SetBlocked(!node.IsBlocked);
            }

            if (input.IsMarkingMode)
            {
                if (button == PointerEventData.InputButton.Left) //lmb
                {
                    _pathSetter.UpdateStartNode(node);
                }
                else if (button == PointerEventData.InputButton.Right) //rmb
                {
                    _pathSetter.UpdateFinishNode(node);
                }
            }

            if (input.IsLinkingMode)
            {
                _visualLinksCreator.TryUseNode(node, button);
            }
        }

        private void OnCellNodeTypeChanged(CellNode node, CellType cellType)
        {
            _pathDrawer.ShowPath(false);
            TryRun(_pathSetter.IsReady);
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

        private void ClearPath(bool b)
        {
            _pathDrawer.ShowPath(false);
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