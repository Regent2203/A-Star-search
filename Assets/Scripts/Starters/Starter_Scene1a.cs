using System.Collections.Generic;
using ThisProject.Implementations.Cells;
using ThisProject.Implementations.Cells.UI;
using ThisProject.Inputs;
using ThisProject.ObjectsStorages;
using ThisProject.PathFinders;
using ThisProject.PathSetters;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace ThisProject.Starters
{
    public class Starter_Scene1a : StarterBase
    {
        private CellsConfig _config;
        private GridTypeStorage<CellNode> _nodes;
        private GridTypeStorage<CellView> _views;
        private CellsClickHandler _clickHandler;
        private CellsFieldGenerator _generator;
        private CellTypeChanger _cellTypeChanger;
        private PathSetter<CellNode> _pathSetter;
        private PathFinder<CellNode> _pathFinder;
        private CellsPathDrawer _pathDrawer;
        private CellsPainter _painter;
        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UICellsHotkeysInfoPanel _hotkeyInfoPanel;


        [Inject]
        public void Construct(CellsConfig config, GridTypeStorage<CellNode> nodes, GridTypeStorage<CellView> views,
            CellsClickHandler clickHandler, CellsFieldGenerator generator,
            CellTypeChanger cellTypeChanger,
            PathSetter<CellNode> pathSetter, PathFinder<CellNode> pathFinder, 
            CellsPathDrawer pathDrawer, CellsPainter painter,
            UICellsPalette palette, UICellsPaletteChoicePanel paletteChoice, UICellsHotkeysInfoPanel hotkeyInfoPanel)
        {
            _config = config;
            _nodes = nodes;
            _views = views;
            _clickHandler = clickHandler;
            _generator = generator;
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
            _clickHandler.NodeViewClicked += OnViewClicked;
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

            _generator.PopulateField(new Vector2Int(13, 11), _config.DefaultCellType);
        }

        protected override void UnsubscribeAll()
        {
            _clickHandler.NodeViewClicked -= OnViewClicked;
            _cellTypeChanger.CellTypeChanged -= OnCellTypeChanged;

            _pathSetter.StartNodeChanged -= OnStartNodeChanged;
            _pathSetter.FinishNodeChanged -= OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged -= OnPathChanged;

            _palette.ItemClicked -= OnPaletteItemClicked;
            _painter.BrushChanged -= OnBrushChanged;
        }


        private void UpdateViewSprite(CellNode node, CellType cellType)
        {
            var view = _views.GetItemById(node.Id);
            view.UpdateSprite(cellType.Sprite);
        }

        private void OnViewClicked(CellView view, PointerEventData.InputButton button, InputSnapshot input)
        {
            var node = _nodes.GetItemById(view.Id);

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
            UpdateViewSprite(node, cellType);
            OnFieldChanged();
        }

        private void OnFieldChanged()
        {
            OnPathChanged(_pathSetter.IsReady);
        }

        private void OnStartNodeChanged(CellNode node, bool b)
        {
            var view = _views.GetItemById(node.Id);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(CellNode node, bool b)
        {
            var view = _views.GetItemById(node.Id);
            view?.ShowFinishMarker(b);
        }

        private void OnPathChanged(bool isReady)
        {
            _pathDrawer.ShowPath(false);
            TryRun(isReady);
        }

        private readonly List<CellView> _viewsPath = new List<CellView>();

        private void TryRun(bool isReady)
        {
            if (isReady)
            {
                var nodesPath = _pathFinder.GetPath(_pathSetter.StartNode, _pathSetter.FinishNode);
                if (nodesPath != null)
                {
                    _views.NodesToViewsNonAlloc(nodesPath, _viewsPath);
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