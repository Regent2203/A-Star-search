using Core.Implementations.Cells;
using Core.Implementations.Cells.UI;
using Core.Inputs;
using Core.PathDrawers;
using Core.PathFinders;
using System;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Starters
{
    public class Starter_Scene1b : IInitializable, IDisposable
    {
        private CellsConfig _config;
        private CellsGridField _field;
        private PathFinder<CellNode> _pathFinder;
        private LinePathDrawer _pathDrawer;
        private CellsPainter _painter;
        private PathSetter<CellNode> _pathSetter;
        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;


        [Inject]
        public void Construct(CellsConfig config, CellsGridField field, PathFinder<CellNode> pathFinder, 
            LinePathDrawer pathDrawer, CellsPainter painter, PathSetter<CellNode> pathSetter,
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

        public void Initialize()
        {
            _field.NodeClicked += OnNodeClicked;
            _field.CellNodeTypeChanged += OnCellNodeTypeChanged;

            _pathFinder.StartNodeChanged += OnStartNodeChanged;
            _pathFinder.FinishNodeChanged += OnFinishNodeChanged;
            _pathFinder.AnyNodeChanged += ClearPath;
            _pathFinder.AnyNodeChanged += TryRun;

            _palette.ItemClicked += OnPaletteItemClicked;

            _painter.LMBBrushSet += OnLMBBrushChanged;
            _painter.RMBBrushSet += OnRMBBrushChanged;

            _painter.SetLMBType(_config.DefaultCellType);
            _painter.SetRMBType(_config.DefaultCellType);
        }

        public void Dispose()
        {
            _field.NodeClicked -= OnNodeClicked;
            _field.CellNodeTypeChanged -= OnCellNodeTypeChanged;

            _pathFinder.StartNodeChanged -= OnStartNodeChanged;
            _pathFinder.FinishNodeChanged -= OnFinishNodeChanged;
            _pathFinder.AnyNodeChanged -= ClearPath;
            _pathFinder.AnyNodeChanged -= TryRun;

            _palette.ItemClicked -= OnPaletteItemClicked;

            _painter.LMBBrushSet -= OnLMBBrushChanged;
            _painter.RMBBrushSet -= OnRMBBrushChanged;
        }

        private void OnNodeClicked(CellNode node, PointerEventData.InputButton button, InputSnapshot input)
        {
            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                _painter.TryChangeCellType(node, button);
            }

            if (input.IsMarkingMode)
            {
                _pathSetter.TryUseNode(node, button);
            }
        }

        private void OnCellNodeTypeChanged(CellNode node, CellType cellType)
        {
            _pathDrawer.ShowPath(false);
            TryRun(_pathFinder.IsReady);
        }

        private void OnStartNodeChanged(CellNode node, bool b)
        {
            var view = _field.GetViewForNode(node);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(CellNode node, bool b)
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

        private void OnPaletteItemClicked(CellType cellType, PointerEventData.InputButton button)
        {
            switch (button)
            {
                case PointerEventData.InputButton.Left:
                    _painter.SetLMBType(cellType);
                    break;
                case PointerEventData.InputButton.Right:
                    _painter.SetRMBType(cellType);
                    break;
            }
        }

        private void OnLMBBrushChanged(CellType cellType)
        {
            _hotkeyInfoPanel.SetLMBText(cellType.Name);
            _paletteChoice.SetLMBChoice(cellType);
        }

        private void OnRMBBrushChanged(CellType cellType)
        {
            _hotkeyInfoPanel.SetRMBText(cellType.Name);
            _paletteChoice.SetRMBChoice(cellType);
        }
    }
}