using Core.Implementations.Cells;
using Core.Implementations.Vertexes;
using Core.Inputs;
using Core.PathDrawers;
using Core.PathFinders;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Starters
{
    public class Starter_Scene2a : IInitializable, IDisposable
    {
        //private CellsConfig _config;
        private VertexesField _field;
        //private GridClickHandler<CellNode> _fieldInputHandler;
        private VertexesVisualLinksCreator _visualLinksCreator;
        private PathFinder<VertexNode> _pathFinder;
        private LinePathDrawer _pathDrawer;
        private PathSetter<VertexNode> _pathSetter;
        //private UIHotkeyInfoPanel_Vertexes _hotkeyInfoPanel;


        [Inject]
        /*
        public void Construct(CellsConfig config, CellsGridField field, GridClickHandler<CellNode> fieldInputHandler,
            palette, UICellsPaletteChoicePanel paletteChoice, UICellsPaletteHotkeyInfoPanel hotkeyInfoPanel)*/
        public void Construct(VertexesField field,VertexesVisualLinksCreator visualLinksCreator,
            PathFinder<VertexNode> pathFinder, LinePathDrawer pathDrawer, PathSetter<VertexNode> pathSetter)
        {
            _field = field;
            _visualLinksCreator = visualLinksCreator;

            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            _pathSetter = pathSetter;
            /*
            _config = config;
            _fieldInputHandler = fieldInputHandler;
            _hotkeyInfoPanel = hotkeyInfoPanel;*/
        }

        public void Initialize()
        {
            _field.NodeClicked += OnNodeClicked;
            //_field.GridTopologyChanged += OnGridTopologyChanged;

            _pathFinder.StartNodeChanged += OnStartNodeChanged;
            _pathFinder.FinishNodeChanged += OnFinishNodeChanged;
            _pathFinder.AnyNodeChanged += ClearPath;
            _pathFinder.AnyNodeChanged += TryRun;

            //NodePositionChanged -> _pathDrawer.ShowPath(false); TryRun(_pathFinder.IsReady);
            //NodeBlockStateChanged -> _pathDrawer.ShowPath(false); TryRun(_pathFinder.IsReady);            

            _pathFinder.AnyNodeChanged += (b) => _pathDrawer.ShowPath(false);
            _pathFinder.StartNodeChanged += (node, b) =>
            {
                var view = _field.GetViewForNode(node);
                view?.ShowStartMarker(b);
            };
            _pathFinder.FinishNodeChanged += (node, b) =>
            {
                var view = _field.GetViewForNode(node);
                view?.ShowFinishMarker(b); 
            };
            
            _pathFinder.AnyNodeChanged += TryRun;

            //NodeClicked += -> LinksCreator.TryUseFirstNode
            //mouseScroll -> ChangeLinkCost

            //temp
            //_visualLinksCreator.TryUseNode(_field.Node1);
            //_visualLinksCreator.TryUseNode(_field.Node2);
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
                _pathSetter.TryUseNode(node, button);
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
            var view = _field.GetViewForNode(node);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(VertexNode node, bool b)
        {
            var view = _field.GetViewForNode(node);
            view?.ShowFinishMarker(b);
        }

        private void ClearPath(bool b)
        {
            _pathDrawer.ShowPath(false);
        }

        private void TryRun(bool isReady)
        {
            if (isReady)
            {
                var nodePath = _pathFinder.GetPath();
                if (nodePath != null)
                {
                    _pathDrawer.SetPath(_field.GetViewsForNodes(nodePath));
                    _pathDrawer.ShowPath(true);
                }
            }
        }
    }
}