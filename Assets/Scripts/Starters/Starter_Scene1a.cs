using Core.Fields.Grids;
using Core.Implementations.Cells;
using Core.Implementations.Cells.UI;
using Core.PathFinders;
using Core.Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Starters
{
    public class Starter_Scene1a : MonoBehaviour
    {
        private SignalBus _signalBus;

        private CellsConfig _config;
        private CellsGridField _field;
        private PathFinder<CellNode> _pathFinder;
        private CellsPathDrawer _pathDrawer;
        private CellsPainter _painter;
        private PathSetter<CellNode> _pathSetter;
        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;


        [Inject]
        public void Construct(SignalBus signalBus, CellsConfig config, CellsGridField field, PathFinder<CellNode> pathFinder, 
            CellsPathDrawer pathDrawer, CellsPainter painter, PathSetter<CellNode> pathSetter,
            UICellsPalette palette, UICellsPaletteChoicePanel paletteChoice, UICellsPaletteHotkeyInfoPanel hotkeyInfoPanel)
        {
            _signalBus = signalBus;
            _config = config;
            _field = field;
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
            _signalBus.Subscribe<NodeClickedSignal<CellNode>>(OnNodeClickedSignal);
            _signalBus.Subscribe<PaletteItemClickedSignal>(OnPaletteItemClickedSignal);

            _field.CellNodeTypeChanged += (_, _) => _pathDrawer.ShowPath(false);
            _field.CellNodeTypeChanged += (_, _) => TryRun(_pathFinder.IsReady);
            
            _pathFinder.AnyNodeChanged += (_) => _pathDrawer.ShowPath(false);
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

            _painter.LMBTypeSet += (cellType) => _hotkeyInfoPanel.SetLMBText(cellType.Name);
            _painter.RMBTypeSet += (cellType) => _hotkeyInfoPanel.SetRMBText(cellType.Name);

            _painter.LMBTypeSet += (cellType) => _paletteChoice.SetLMBChoice(cellType);
            _painter.RMBTypeSet += (cellType) => _paletteChoice.SetRMBChoice(cellType);

            _painter.SetLMBType(_config.DefaultCellType);
            _painter.SetRMBType(_config.DefaultCellType);
        }

        private void OnNodeClickedSignal(NodeClickedSignal<CellNode> signal)
        {
            var input = signal.Input;
            
            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                _painter.TryChangeCellType(signal.Node, signal.Button);
            }

            if (input.IsMarkingMode)
            {
                _pathSetter.TryUseNode(signal.Node, signal.Button);
            }
        }

        private void OnPaletteItemClickedSignal(PaletteItemClickedSignal signal)
        {
            switch (signal.Button)
            {
                case PointerEventData.InputButton.Left:
                    _painter.SetLMBType(signal.CellType);
                    break;
                case PointerEventData.InputButton.Right:
                    _painter.SetRMBType(signal.CellType);
                    break;
            }
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