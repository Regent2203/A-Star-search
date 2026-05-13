using Core.Fields.Grids;
using Core.Implementations.Cells;
using Core.Implementations.Cells.UI;
using Core.PathDrawers;
using Core.PathFinders;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Starters
{
    public class Starter_VertexFieldExample1 : MonoBehaviour
    {
        private CellsConfig _config;
        private CellsGridField _field;
        private GridInputHandler<CellNode> _fieldInputHandler;
        private PathFinder<CellNode> _pathFinder;
        private LinePathDrawer _pathDrawer;
        private CellsPainter _painter;
        private PathSetter<CellNode> _pathSetter;
        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;


        [Inject]
        public void Construct(CellsConfig config, CellsGridField field, GridInputHandler<CellNode> fieldInputHandler,
            PathFinder<CellNode> pathFinder, LinePathDrawer pathDrawer, CellsPainter painter, PathSetter<CellNode> pathSetter,
            UICellsPalette palette, UICellsPaletteChoicePanel paletteChoice, UICellsPaletteHotkeyInfoPanel hotkeyInfoPanel)
        {
            _config = config;
            _field = field;
            _fieldInputHandler = fieldInputHandler;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            _painter = painter;
            _pathSetter = pathSetter;
            _palette = palette;
            _paletteChoice = paletteChoice;
            _hotkeyInfoPanel = hotkeyInfoPanel;
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

        private void OnPaletteItemClicked(CellType cellType, PointerEventData.InputButton btn)
        {
            if (btn == PointerEventData.InputButton.Left) //lmb
            {
                _painter.SetLMBType(cellType);

            }
            else if (btn == PointerEventData.InputButton.Right) //rmb
            {
                _painter.SetRMBType(cellType);
            }
        }
    }
}