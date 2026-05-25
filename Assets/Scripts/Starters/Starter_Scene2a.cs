using ThisProject.Implementations.Cells;
using ThisProject.Implementations.Vertexes;
using ThisProject.Inputs;
using ThisProject.PathDrawers;
using ThisProject.PathFinders;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Starters
{
    public class Starter_Scene2a : IInitializable, IDisposable
    {
        //private CellsConfig _config;
        private VertexesField _field;
        //private GridFieldClickHandler<CellNode> _fieldInputHandler;
        private VertexesVisualLinksCreator _visualLinksCreator;
        private PathFinder<VertexNode> _pathFinder;
        private LinePathDrawer _pathDrawer;
        //private UIHotkeyInfoPanel_Vertexes _hotkeyInfoPanel;


        [Inject]
        /*
        public void Construct(CellsConfig config, CellsGridField field, GridFieldClickHandler<CellNode> fieldInputHandler,
            palette, UICellsPaletteChoicePanel paletteChoice, UICellsPaletteHotkeyInfoPanel hotkeyInfoPanel)*/
        public void Construct(VertexesField field,VertexesVisualLinksCreator visualLinksCreator,
            PathFinder<VertexNode> pathFinder, LinePathDrawer pathDrawer)
        {
            _field = field;
            _visualLinksCreator = visualLinksCreator;

            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            /*
            _config = config;
            _fieldInputHandler = fieldInputHandler;
            _hotkeyInfoPanel = hotkeyInfoPanel;*/
        }

        public void Initialize()
        {
            _field.NodeClicked += OnNodeClicked;
            //_visual.FieldChanged += OnFieldChanged;

            _pathFinder.StartNodeChanged += OnStartNodeChanged;
            _pathFinder.FinishNodeChanged += OnFinishNodeChanged;
            _pathFinder.AnyNodeChanged += ClearPath;
            _pathFinder.AnyNodeChanged += TryRun;

            //NodePositionChanged -> _pathDrawer.ShowPath(false); TryRun(_pathFinder.IsReady);
            //NodeBlockStateChanged -> _pathDrawer.ShowPath(false); TryRun(_pathFinder.IsReady);            

            _pathFinder.AnyNodeChanged += (b) => _pathDrawer.ShowPath(false);
            _pathFinder.StartNodeChanged += (node, b) =>
            {
                var view = _field.Visual.GetViewById(node.Id);
                view?.ShowStartMarker(b);
            };
            _pathFinder.FinishNodeChanged += (node, b) =>
            {
                var view = _field.Visual.GetViewById(node.Id);
                view?.ShowFinishMarker(b); 
            };
            
            _pathFinder.AnyNodeChanged += TryRun;

            //NodeClicked += -> LinksCreator.TryUseFirstNode
            //mouseScroll -> ChangeLinkCost

            //temp
            //_visualLinksCreator.TryUseNode(_visual.Node1);
            //_visualLinksCreator.TryUseNode(_visual.Node2);
        }

        public void Dispose()
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
                    _pathFinder.UpdateStartNode(node);
                }
                else if (button == PointerEventData.InputButton.Right) //rmb
                {
                    _pathFinder.UpdateFinishNode(node);
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
            TryRun(_pathFinder.IsReady);
        }

        private void OnStartNodeChanged(VertexNode node, bool b)
        {
            var view = _field.Visual.GetViewById(node.Id);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(VertexNode node, bool b)
        {
            var view = _field.Visual.GetViewById(node.Id);
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
                var nodesPath = _pathFinder.GetPath();
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