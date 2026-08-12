using EasyField.BrushManagers;
using EasyField.Implementations.Cells;
using EasyField.Implementations.Cells.Core.Dto;
using EasyField.Implementations.Cells.UI;
using EasyField.Inputs;
using EasyField.PathDrawers;
using EasyField.PathFinders;
using EasyField.PathSetters;
using EasyField.UICommon;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.SceneControllers
{
    public class SceneController_Scene1b : SceneControllerBase
    {
        private CellsFieldBuilder _fieldBuilder;
        private CellDataStorage _nodeDatas;
        private CellViewStorage _nodeViews;

        private CellsConfig _config;
        private CellsClickHandler _clickHandler;        
        private CellTypeChanger _cellTypeChanger;
        private BrushManager<CellType> _cellTypebrushManager;

        private PathSetter<CellData> _pathSetter;
        private PathFinder<CellData, Vector2Int> _pathFinder;
        private IPathDrawer<CellView> _pathDrawer;

        private CellsSaveLoadManager _saveLoadManager;
        private UIMainButtonsPanel _saveLoadPanel;        

        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UIHotkeysInfoPanel_Cells _hotkeyInfoPanel;


        [Inject]
        public void Construct(CellsFieldBuilder fieldBuilder, CellDataStorage nodeDatas, CellViewStorage nodeViews,
            CellsConfig config, CellsClickHandler clickHandler, 
            CellTypeChanger cellTypeChanger, BrushManager<CellType> cellTypebrushManager,
            PathSetter<CellData> pathSetter, PathFinder<CellData, Vector2Int> pathFinder, IPathDrawer<CellView> pathDrawer,
            CellsSaveLoadManager saveLoadManager, UIMainButtonsPanel saveLoadPanel,
            UICellsPalette palette, UICellsPaletteChoicePanel paletteChoice, UIHotkeysInfoPanel_Cells hotkeyInfoPanel)
        {
            _config = config;
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;
            _clickHandler = clickHandler;
            _fieldBuilder = fieldBuilder;
            _cellTypeChanger = cellTypeChanger;
            _cellTypebrushManager = cellTypebrushManager;

            _pathSetter = pathSetter;
            _pathFinder = pathFinder;
            _pathDrawer = pathDrawer;            
            
            _saveLoadManager = saveLoadManager;
            _saveLoadPanel = saveLoadPanel;

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

            _saveLoadPanel.SaveBtnClicked += OnSaveBtnClicked;
            _saveLoadPanel.LoadBtnClicked += OnLoadBtnClicked;
            _saveLoadPanel.NewBtnClicked += OnNewBtnClicked;

            _palette.ItemClicked += OnPaletteItemClicked;
            _cellTypebrushManager.BrushChanged += OnBrushChanged;
        }

        protected override void InitDefaultStates()
        {
            _cellTypebrushManager.SetBrush(1, _config.DefaultCellType);
            _cellTypebrushManager.SetBrush(2, _config.DefaultCellType);

            _fieldBuilder.PopulateField(new Vector2Int(13, 11), _config.DefaultCellType);
        }

        protected override void UnsubscribeAll()
        {
            _clickHandler.NodeViewClicked -= OnViewClicked;
            _cellTypeChanger.CellTypeChanged -= OnCellTypeChanged;

            _pathSetter.StartNodeChanged -= OnStartNodeChanged;
            _pathSetter.FinishNodeChanged -= OnFinishNodeChanged;
            _pathSetter.AnyNodeChanged -= OnPathChanged;

            _saveLoadPanel.SaveBtnClicked -= OnSaveBtnClicked;
            _saveLoadPanel.LoadBtnClicked -= OnLoadBtnClicked;
            _saveLoadPanel.NewBtnClicked -= OnNewBtnClicked;

            _palette.ItemClicked -= OnPaletteItemClicked;
            _cellTypebrushManager.BrushChanged -= OnBrushChanged;
        }


        private void UpdateViewSprite(CellData node, CellType cellType)
        {
            var view = _nodeViews.GetItem(node.Id);
            view.UpdateSprite(cellType.Sprite);
        }

        private void OnViewClicked(CellView view, PointerEventData.InputButton button, InputSnapshot input)
        {
            var node = _nodeDatas.GetItem(view.Id);

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
            var view = _nodeViews.GetItem(node.Id);
            view?.ShowStartMarker(b);
        }

        private void OnFinishNodeChanged(CellData node, bool b)
        {
            var view = _nodeViews.GetItem(node.Id);
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
                    _nodeViews.NodesToViewsNonAlloc(nodesPath, _viewsPath);
                    _pathDrawer.SetPath(_viewsPath);
                    _pathDrawer.ShowPath(true);
                }
            }
        }

        private void OnSaveBtnClicked()
        {
            _saveLoadManager.StartSaving();
        }

        private async void OnLoadBtnClicked()
        {
            var dto = await _saveLoadManager.StartLoading();
            _fieldBuilder.BuildFromDto(dto);
        }

        private void OnNewBtnClicked(int sizeX, int sizeY)
        {
            _fieldBuilder.CreateNewField(sizeX, sizeY);
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