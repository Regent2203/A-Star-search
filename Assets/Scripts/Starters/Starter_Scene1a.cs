using System.Collections.Generic;
using ThisProject.Implementations.Cells;
using ThisProject.Implementations.Cells.UI;
using ThisProject.Inputs;
using ThisProject.PathFinders;
using ThisProject.PathSetters;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Starters
{
    public class Starter_Scene1a : StarterBase
    {
        private CellsConfig _config;
        private CellsField _field;
        private CellsClickHandler _clickHandler;
        private CellTypeChanger _cellTypeChanger;
        private PathSetter<CellNode> _pathSetter;
        private PathFinder<CellNode> _pathFinder;
        private CellsPathDrawer _pathDrawer;
        private CellsPainter _painter;
        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;


        [Inject]
        public void Construct(CellsConfig config, CellsField field, CellsClickHandler clickHandler, CellTypeChanger cellTypeChanger,
            PathSetter<CellNode> pathSetter, PathFinder<CellNode> pathFinder, 
            CellsPathDrawer pathDrawer, CellsPainter painter,
            UICellsPalette palette, UICellsPaletteChoicePanel paletteChoice, UICellsPaletteHotkeyInfoPanel hotkeyInfoPanel)
        {
            _config = config;
            _field = field;
            _clickHandler = clickHandler;
            _cellTypeChanger = cellTypeChanger;
            _pathSetter = pathSetter;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            _painter = painter;
            _palette = palette;
            _paletteChoice = paletteChoice;
            _hotkeyInfoPanel = hotkeyInfoPanel;
        }

        protected override void SubscribeAll()
        {
            _clickHandler.NodeClicked += OnNodeClicked;
            _cellTypeChanger.CellTypeChanged += OnCellTypeChanged;

            _pathSetter.StartNodeChanged += OnStartNodeChanged;
            _pathSetter.FinishNodeChanged += OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged += OnPathChanged;

            _palette.ItemClicked += OnPaletteItemClicked;
            _painter.BrushChanged += OnBrushChanged;
        }

        protected override void InitDefaultStates()
        {
            _painter.SetBrush(BrushType.Primary, _config.DefaultCellType);
            _painter.SetBrush(BrushType.Secondary, _config.DefaultCellType);
        }

        protected override void UnsubscribeAll()
        {
            _clickHandler.NodeClicked -= OnNodeClicked;
            _cellTypeChanger.CellTypeChanged -= OnCellTypeChanged;

            _pathSetter.StartNodeChanged -= OnStartNodeChanged;
            _pathSetter.FinishNodeChanged -= OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged -= OnPathChanged;

            _palette.ItemClicked -= OnPaletteItemClicked;
            _painter.BrushChanged -= OnBrushChanged;
        }


        private void UpdateView(CellNode node, CellType cellType)
        {
            var view = _field.GetViewById(node.Id);
            view.UpdateSprite(cellType.Sprite);
        }

        private void OnNodeClicked(CellNode node, PointerEventData.InputButton button, InputSnapshot input)
        {
            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                switch (button)
                {
                    case PointerEventData.InputButton.Left:
                        _painter.PaintCell(node, BrushType.Primary);
                        break;
                    case PointerEventData.InputButton.Right:
                        _painter.PaintCell(node, BrushType.Secondary);
                        break;
                }
            }

            if (input.IsMarkingMode)
            {
                switch (button)
                {
                    case PointerEventData.InputButton.Left:
                        _pathSetter.UpdateStartNode(node);
                        break;
                    case PointerEventData.InputButton.Right:
                        _pathSetter.UpdateFinishNode(node);
                        break;
                }
            }
        }

        private void OnCellTypeChanged(CellNode node, CellType cellType)
        {
            UpdateView(node, cellType);
            OnFieldChanged();
        }

        private void OnFieldChanged()
        {
            OnPathChanged(_pathSetter.IsReady);
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
                var nodesPath = _pathFinder.GetPath(_pathSetter.StartNode, _pathSetter.FinishNode);
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