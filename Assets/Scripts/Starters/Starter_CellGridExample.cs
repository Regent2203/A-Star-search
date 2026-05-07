using Core.CostProviders;
using Core.Fields.Grids;
using Core.Heuristic;
using Core.Implementations.Cells;
using Core.Implementations.Cells.UI;
using Core.PathFinders;
using Core.SearchAlgorithms;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Starters
{
    public class Starter_CellGridExample : MonoBehaviour
    {
        private CellsConfig _config;
        private CellsGridField _field;
        private CellsGridInputHandler _fieldInputHandler;
        private PathFinder<CellNode> _pathFinder;
        private CellsPathDrawer _pathDrawer;
        private CellsPainter _painter;
        private CellsPathSetter _pathSetter;
        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;


        [Inject]
        public void Construct(CellsConfig config, CellsGridField field, CellsGridInputHandler fieldInputHandler,
            PathFinder<CellNode> pathFinder, CellsPathDrawer pathDrawer, CellsPainter painter, CellsPathSetter pathSetter,
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
            _fieldInputHandler.CellNodeClicked += _painter.TryChangeCellType;
            _fieldInputHandler.CellNodeClicked += _pathSetter.TryUseCell;
            _field.CellNodeTypeChanged += (_, _) => _pathDrawer.ShowPath(false);
            _field.CellNodeTypeChanged += (_, _) => TryRun(_pathFinder.IsReady);
            
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

        private void TryRun(bool isReady)
        {
            if (isReady)
            {
                var nodePath = _pathFinder.GetPath();
                if (nodePath != null)
                {
                    _pathDrawer.SetPath(nodePath, _field);
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