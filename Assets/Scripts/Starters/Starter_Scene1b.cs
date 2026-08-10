using EasyField.BrushManagers;
using EasyField.Implementations.Cells;
using EasyField.Implementations.Cells.UI;
using EasyField.Inputs;
using EasyField.PathDrawers;
using EasyField.PathFinders;
using EasyField.PathSetters;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.Starters
{
    public class Starter_Scene1b : StarterBase
    {
        private CellsConfig _config;
        private CellDataStorage _nodes;
        private CellViewStorage _views;
        private CellsClickHandler _clickHandler;
        private CellsFieldBuilder _builder;
        private CellTypeChanger _cellTypeChanger;
        private PathSetter<CellData> _pathSetter;
        private PathFinder<CellData, Vector2Int> _pathFinder;
        private IPathDrawer<CellView> _pathDrawer;
        private BrushManager<CellType> _cellTypebrushManager;
        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UIHotkeysInfoPanel_Cells _hotkeyInfoPanel;


        [Inject]
        public void Construct(CellsConfig config, CellDataStorage nodes, CellViewStorage views,
            CellsClickHandler clickHandler, CellsFieldBuilder builder,
            CellTypeChanger cellTypeChanger,
            PathSetter<CellData> pathSetter, PathFinder<CellData, Vector2Int> pathFinder,
            IPathDrawer<CellView> pathDrawer, BrushManager<CellType> cellTypebrushManager,
            UICellsPalette palette, UICellsPaletteChoicePanel paletteChoice, UIHotkeysInfoPanel_Cells hotkeyInfoPanel)
        {
            _config = config;
            _nodes = nodes;
            _views = views;
            _clickHandler = clickHandler;
            _builder = builder;
            _cellTypeChanger = cellTypeChanger;
            _pathSetter = pathSetter;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;
            _cellTypebrushManager = cellTypebrushManager;
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
            _cellTypebrushManager.BrushChanged += OnBrushChanged;
        }

        protected override void InitDefaultStates()
        {
            _cellTypebrushManager.SetBrush(1, _config.DefaultCellType);
            _cellTypebrushManager.SetBrush(2, _config.DefaultCellType);

            _builder.PopulateField(new Vector2Int(12, 10), _config.DefaultCellType);
        }

        protected override void UnsubscribeAll()
        {
            _clickHandler.NodeViewClicked -= OnViewClicked;
            _cellTypeChanger.CellTypeChanged -= OnCellTypeChanged;

            _pathSetter.StartNodeChanged -= OnStartNodeChanged;
            _pathSetter.FinishNodeChanged -= OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged -= OnPathChanged;

            _palette.ItemClicked -= OnPaletteItemClicked;
            _cellTypebrushManager.BrushChanged -= OnBrushChanged;
        }


        private void UpdateViewSprite(CellData node, CellType cellType)
        {
            var view = _views.GetItem(node.Id);
            view.UpdateSprite(cellType.Sprite);
        }

        private void OnViewClicked(CellView view, PointerEventData.InputButton button, InputSnapshot input)
        {
            var node = _nodes.GetItem(view.Id);

            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                switch (button)
                {
                    case PointerEventData.InputButton.Left:
                        _cellTypeChanger.TryChangeCellType(node, _cellTypebrushManager.GetBrush(1));
                        break;
                    case PointerEventData.InputButton.Right:
                        _cellTypeChanger.TryChangeCellType(node, _cellTypebrushManager.GetBrush(2));
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

        private void OnCellTypeChanged(CellData node, CellType cellType)
        {
            UpdateViewSprite(node, cellType);
            OnFieldChanged();
        }

        private void OnFieldChanged()
        {
            OnPathChanged(_pathSetter.IsReady);
        }

        private void OnStartNodeChanged(CellData node, bool b)
        {
            var view = _views.GetItem(node.Id);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(CellData node, bool b)
        {
            var view = _views.GetItem(node.Id);
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
                    _cellTypebrushManager.SetBrush(1, cellType);
                    break;
                case PointerEventData.InputButton.Right:
                    _cellTypebrushManager.SetBrush(2, cellType);
                    break;
            }
        }

        private void OnBrushChanged(int brushIndex, CellType cellType)
        {
            switch (brushIndex)
            {
                case 1:
                    _hotkeyInfoPanel.SetLMBPaintText(cellType.Name);
                    _paletteChoice.SetLMBChoice(cellType);
                    break;
                case 2:
                    _hotkeyInfoPanel.SetRMBPaintText(cellType.Name);
                    _paletteChoice.SetRMBChoice(cellType);
                    break;
            }
        }
    }
}