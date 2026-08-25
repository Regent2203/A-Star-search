using EasyField.BrushManagers;
using EasyField.Implementations.Cells;
using EasyField.Implementations.Cells.Core.Dto;
using EasyField.Implementations.Cells.UI;
using EasyField.Inputs;
using EasyField.Links;
using EasyField.Links.Providers;
using EasyField.PathSetters;
using EasyField.UICommon;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace EasyField.SceneControllers
{
    public class SceneController_Scene4a : SceneControllerBase
    {        
        private CellsFieldBuilder _fieldBuilder;
        private CellDataStorage _nodeDatas;
        private CellViewStorage _nodeViews;

        private CombinedLinksProvider<CellData, LinkData<Vector2Int>> _linksProvider;

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
            CombinedLinksProvider<CellData, LinkData<Vector2Int>> linksProvider,
            CellsConfig config, CellsClickHandler clickHandler, 
            CellTypeChanger cellTypeChanger, BrushManager<CellType> cellTypebrushManager,
            PathSetter<CellData> pathSetter, CellsPathfindRunner pathfindRunner,
            CellsSaveLoadManager saveLoadManager, UIMainButtonsPanel saveLoadPanel,
            UICellsPalette palette, UICellsPaletteChoicePanel paletteChoice, UIHotkeysInfoPanel_Cells hotkeyInfoPanel)
        {
            _fieldBuilder = fieldBuilder;            
            _nodeDatas = nodeDatas;
            _nodeViews = nodeViews;

            _linksProvider = linksProvider;

            _config = config;
            _clickHandler = clickHandler;            
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
            _clickHandler.FieldClicked += OnFieldClicked;
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
            _clickHandler.FieldClicked -= OnFieldClicked;
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

        private void OnFieldClicked(Vector2 pos, PointerEventData.InputButton button, InputSnapshot input)
        {
            if (input.IsCreatingMode)
            {
                if (button == PointerEventData.InputButton.Left)
                    if (_fieldBuilder.TryCreateNode(pos))
                        OnFieldChanged();
            }
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

            if (input.IsCreatingMode)
            {
                if (button == PointerEventData.InputButton.Right)
                    if (_fieldBuilder.TryDeleteNode(nodeView.Id))
                        OnFieldChanged();
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
            if (dto != null)
            {
                _fieldBuilder.BuildFromDto(dto);
                //todo
            }
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