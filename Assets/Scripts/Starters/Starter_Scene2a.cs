using Core.Implementations.Vertexes;
using Core.PathDrawers;
using Core.PathFinders;
using UnityEngine;
using Zenject;

namespace Core.Starters
{
    public class Starter_Scene2a : MonoBehaviour
    {
        //private CellsConfig _config;
        private VertexesField _field;
        //private GridInputHandler<CellNode> _fieldInputHandler;
        VertexesVisualLinksCreator _visualLinksManager;
        private PathFinder<VertexNode> _pathFinder;
        private LinePathDrawer _pathDrawer;
        private PathSetter<VertexNode> _pathSetter;
        //private UIHotkeyInfoPanel_Vertexes _hotkeyInfoPanel;


        [Inject]
        /*
        public void Construct(CellsConfig config, CellsGridField field, GridInputHandler<CellNode> fieldInputHandler,
            palette, UICellsPaletteChoicePanel paletteChoice, UICellsPaletteHotkeyInfoPanel hotkeyInfoPanel)*/
        public void Construct(VertexesField field,VertexesVisualLinksCreator visualLinksManager,
            PathFinder<VertexNode> pathFinder, LinePathDrawer pathDrawer, PathSetter<VertexNode> pathSetter)
        {
            _field = field;
            _visualLinksManager = visualLinksManager;

            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            _pathSetter = pathSetter;
            /*
            _config = config;
            _fieldInputHandler = fieldInputHandler;
            _hotkeyInfoPanel = hotkeyInfoPanel;*/
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            //NodeClicked += //change blocked state
            //NodeClicked += _pathSetter.TryUseNode;
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
            //_visualLinksManager.TryUseNode(_field.Node1);
            //_visualLinksManager.TryUseNode(_field.Node2);
        }

        private void TryRun(bool isReady)
        {
            if (isReady)
            {
                var nodePath = _pathFinder.GetPath();
                if (nodePath != null)
                {
                    var viewPath = _field.GetViewsForNodes(nodePath);
                    _pathDrawer.SetPath(viewPath);
                    _pathDrawer.ShowPath(true);
                }
            }
        }
    }
}