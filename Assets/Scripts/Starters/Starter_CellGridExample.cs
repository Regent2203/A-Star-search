using Core.CostProviders;
using Core.Heuristic;
using Core.Implementations.Cells;
using Core.Implementations.Cells.UI;
using Core.PathFinders;
using Core.SearchAlgorithms;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Starters
{
    public class Starter_CellGridExample : MonoBehaviour
    {
        private CellsConfig _config;
        private CellsGridField _field;
        private PathFinder _pathFinder;
        private CellsPathDrawer _pathDrawer;
        private CellsPainter _painter;
        private CellsPathSetter _pathSetter;
        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;


        [Inject]
        public void Construct(CellsConfig config, CellsGridField field,
            PathFinder pathFinder, CellsPathDrawer pathDrawer, CellsPainter painter, CellsPathSetter pathSetter,
            UICellsPalette palette, UICellsPaletteChoicePanel paletteChoice, UICellsPaletteHotkeyInfoPanel hotkeyInfoPanel)
        {
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
            _field.CellClicked += _painter.TryChangeCellType;
            _field.CellClicked += _pathSetter.TryUseCell;
            //_field.CellTypeChanged += (_, _) => _pathDrawer.ShowPath(false);
            //_field.CellTypeChanged += (_, _) => TryRun();
            
            
            _pathFinder.NodeChanged += () => _pathDrawer.ShowPath(false);
            
            //_pathFinder.StartNodeChanged += (cell, b) => cell?.ShowStartMarker(b);
            //_pathFinder.FinishNodeChanged += (cell, b) => cell?.ShowFinishMarker(b);
            
            _pathFinder.NodeChanged += TryRun;

            _painter.LMBTypeSet += (cellType) => _hotkeyInfoPanel.SetLMBText(cellType.Name);
            _painter.RMBTypeSet += (cellType) => _hotkeyInfoPanel.SetRMBText(cellType.Name);

            _painter.LMBTypeSet += (cellType) => _paletteChoice.SetLMBChoice(cellType);
            _painter.RMBTypeSet += (cellType) => _paletteChoice.SetRMBChoice(cellType);

            foreach (var item in _palette.AllItems)
            {
                item.ItemClicked += OnPaletteItemClicked;
            }

            _painter.SetLMBType(_config.DefaultCellType);
            _painter.SetRMBType(_config.DefaultCellType);
        }

        private void TryRun()
        {
            if (_pathFinder.IsReady)
            {
                var path = _pathFinder.GetPath();
                //_field.TryGetView(path[0].index
                //_pathDrawer.SetPath(path);
                //_pathDrawer.ShowPath(true); //todo
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