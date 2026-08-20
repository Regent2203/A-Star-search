using EasyField.BrushManagers;
using EasyField.Implementations.Cells;
using EasyField.Implementations.Cells.Core.Dto;
using EasyField.Implementations.Cells.UI;
using EasyField.Inputs;
using EasyField.PathSetters;
using EasyField.UICommon;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.SceneControllers
{
    public class SceneController_Scene1a : SceneControllerBase
    {        
        private CellsFieldBuilder _fieldBuilder;
        private CellDataStorage _nodeDatas;
        private CellViewStorage _nodeViews;

        private CellsConfig _config;
        private CellsClickHandler _clickHandler;
        private CellTypeChanger _cellTypeChanger;
        private BrushManager<CellType> _cellTypebrushManager;

        private PathSetter<CellData> _pathSetter;        
        private CellsPathfindRunner _pathfindRunner;

        private CellsSaveLoadManager _saveLoadManager;
        private UIMainButtonsPanel _saveLoadPanel;
        
        private UICellsPalette _palette;
        private UICellsPaletteChoicePanel _paletteChoice;
        private UIHotkeysInfoPanel_Cells _hotkeyInfoPanel;


        [Inject]
        public void Construct(CellsFieldBuilder fieldBuilder, CellDataStorage nodeDatas, CellViewStorage nodeViews,
            CellsConfig config, CellsClickHandler clickHandler, 
            CellTypeChanger cellTypeChanger, BrushManager<CellType> cellTypebrushManager,
            PathSetter<CellData> pathSetter, CellsPathfindRunner pathfindRunner,
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
            _pathfindRunner = pathfindRunner;

            _saveLoadManager = saveLoadManager;
            _saveLoadPanel = saveLoadPanel;

            _palette = palette;
            _paletteChoice = paletteChoice;
            _hotkeyInfoPanel = hotkeyInfoPanel;
        }

        protected override void SubscribeAll()
        {
            _clickHandler.NodeViewClicked += OnNodeViewClicked;
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

            _fieldBuilder.CreateNewField(8, 8);
        }

        protected override void UnsubscribeAll()
        {
            _clickHandler.NodeViewClicked -= OnNodeViewClicked;
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


        private void UpdateViewSprite(CellData nodeData, CellType cellType)
        {
            var nodeView = _nodeViews.GetItem(nodeData.Id);
            nodeView.UpdateSprite(cellType.Sprite);
        }

        private void OnNodeViewClicked(CellView nodeView, PointerEventData.InputButton button, InputSnapshot input)
        {
            var nodeData = _nodeDatas.GetItem(nodeView.Id);

            if (!input.IsMarkingMode && !input.IsCreatingMode && !input.IsLinkingMode)
            {
                switch (button)
                {
                    case PointerEventData.InputButton.Left:
                        _cellTypeChanger.TryChangeCellType(nodeData, _cellTypebrushManager.GetBrush(1));
                        break;
                    case PointerEventData.InputButton.Right:
                        _cellTypeChanger.TryChangeCellType(nodeData, _cellTypebrushManager.GetBrush(2));
                        break;
                }
            }

            if (input.IsMarkingMode)
            {
                switch (button)
                {
                    case PointerEventData.InputButton.Left:
                        _pathSetter.UpdateStartNode(nodeData);
                        break;
                    case PointerEventData.InputButton.Right:
                        _pathSetter.UpdateFinishNode(nodeData);
                        break;
                }
            }
        }

        private void OnCellTypeChanged(CellData nodeData, CellType cellType)
        {
            UpdateViewSprite(nodeData, cellType);
            OnFieldChanged();
        }

        private void OnFieldChanged()
        {
            OnPathChanged(_pathSetter.IsReady);
        }

        private void OnStartNodeChanged(CellData nodeData, bool b)
        {
            if (nodeData != null)
            {
                var nodeView = _nodeViews.GetItem(nodeData.Id);
                nodeView?.ShowStartMarker(b);
            }
        }

        private void OnFinishNodeChanged(CellData nodeData, bool b)
        {
            if (nodeData != null)
            {
                var nodeView = _nodeViews.GetItem(nodeData.Id);
                nodeView?.ShowFinishMarker(b);
            }
        }

        private void OnPathChanged(bool isReady)
        {
            _pathfindRunner.ProcessChanges(isReady);
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