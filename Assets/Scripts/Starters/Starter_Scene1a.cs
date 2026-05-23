using Core.Implementations.Cells;
using Core.Implementations.Cells.UI;
using Core.Inputs;
using Core.PathFinders;
using System;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine.EventSystems;
using Zenject;

namespace Core.Starters
{
    public class Starter_Scene1a : IInitializable, IDisposable
    {
        private CellsConfig _config;
        private CellsGridField _field;
        private PathFinder<CellNode> _pathFinder;
        private CellsPathDrawer _pathDrawer;
        private CellsPainter _painter;
        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;


        [Inject]
        public void Construct(CellsConfig config, CellsGridField field, PathFinder<CellNode> pathFinder, 
            CellsPathDrawer pathDrawer, CellsPainter painter,
            UICellsPalette palette, UICellsPaletteChoicePanel paletteChoice, UICellsPaletteHotkeyInfoPanel hotkeyInfoPanel)
        {
            _config = config;
            _field = field;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            _painter = painter;
            _palette = palette;
            _paletteChoice = paletteChoice;
            _hotkeyInfoPanel = hotkeyInfoPanel;
        }

        public void Initialize()
        {
            _field.NodeClicked += OnNodeClicked;
            _field.FieldChanged += OnFieldChanged;

            _pathFinder.StartNodeChanged += OnStartNodeChanged;
            _pathFinder.FinishNodeChanged += OnFinishNodeChanged;
            _pathFinder.AnyNodeChanged += OnPathChanged;

            _palette.ItemClicked += OnPaletteItemClicked;
            _painter.BrushChanged += OnBrushChanged;

            _painter.SetBrush(BrushType.Primary, _config.DefaultCellType);
            _painter.SetBrush(BrushType.Secondary, _config.DefaultCellType);
        }

        public void Dispose()
        {
            _field.NodeClicked -= OnNodeClicked;
            _field.FieldChanged -= OnFieldChanged;

            _pathFinder.StartNodeChanged -= OnStartNodeChanged;
            _pathFinder.FinishNodeChanged -= OnFinishNodeChanged;
            _pathFinder.AnyNodeChanged -= OnPathChanged;

            _palette.ItemClicked -= OnPaletteItemClicked;
            _painter.BrushChanged -= OnBrushChanged;
        }

        private void OnNodeClicked(CellNode node, PointerEventData.InputButton button, InputSnapshot input)
        {
            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                switch (button)
                {
                    case PointerEventData.InputButton.Left:
                        _painter.TryChangeCellType(node, BrushType.Primary);
                        break;
                    case PointerEventData.InputButton.Right:
                        _painter.TryChangeCellType(node, BrushType.Secondary);
                        break;
                }
            }

            if (input.IsMarkingMode)
            {
                switch (button)
                {
                    case PointerEventData.InputButton.Left:
                        _pathFinder.UpdateStartNode(node);
                        break;
                    case PointerEventData.InputButton.Right:
                        _pathFinder.UpdateFinishNode(node);
                        break;
                }
            }
        }

        private void OnFieldChanged()
        {
            OnPathChanged(_pathFinder.IsReady);
        }

        private void OnStartNodeChanged(CellNode node, bool b)
        {
            var view = _field.GetViewById(node.Id);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(CellNode node, bool b)
        {
            var view = _field.GetViewById(node.Id);
            view?.ShowFinishMarker(b);
        }

        private void OnPathChanged(bool isReady)
        {
            _pathDrawer.ShowPath(false);
            TryRun(isReady);
        }

        private List<CellView> _viewsPath = new List<CellView>();

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

        private void OnPaletteItemClicked(CellType cellType, PointerEventData.InputButton button)
        {
            switch (button)
            {
                case PointerEventData.InputButton.Left:
                    _painter.SetBrush(BrushType.Primary, cellType);
                    break;
                case PointerEventData.InputButton.Right:
                    _painter.SetBrush(BrushType.Secondary, cellType);
                    break;
            }
        }

        private void OnBrushChanged(BrushType brush, CellType cellType)
        {
            switch (brush)
            {
                case BrushType.Primary:
                    _hotkeyInfoPanel.SetLMBText(cellType.Name);
                    _paletteChoice.SetLMBChoice(cellType);
                    break;
                case BrushType.Secondary:
                    _hotkeyInfoPanel.SetRMBText(cellType.Name);
                    _paletteChoice.SetRMBChoice(cellType);
                    break;
            }
        }
    }
}