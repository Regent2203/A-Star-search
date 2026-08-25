using EasyField.BrushManagers;
using EasyField.Fields;
using EasyField.GridNeighbours;
using EasyField.Heuristic.Functions;
using EasyField.Implementations.Cells;
using EasyField.Implementations.Cells.Core.Dto;
using EasyField.Implementations.Cells.UI;
using EasyField.Inputs;
using EasyField.Links;
using EasyField.Links.CostProviders;
using EasyField.Links.Factories;
using EasyField.Links.Implementations;
using EasyField.Links.Providers;
using EasyField.ObjectsStorages;
using EasyField.PathFinders;
using EasyField.PathSetters;
using EasyField.SaveSystem;
using EasyField.SaveSystem.FileDtoGateways;
using EasyField.SaveSystem.FilePathProviders;
using EasyField.SceneControllers;
using EasyField.SearchAlgorithms;
using EasyField.Serializers;
using EasyField.UICommon;
using System;
using UnityEngine;
using Zenject;

namespace EasyField.SceneInstallers
{
    public class SceneInstaller_Scene1a : MonoInstaller
    {
        [SerializeField]
        private Camera _mainCamera;
        [SerializeField]
        private InputSettings _inputSettings;
        [SerializeField]
        private CellView _cellViewPrefab;
        [SerializeField]
        private RectGridField _field;
        [SerializeField]
        private CellsClickHandler _clickHandler;
        [SerializeField]
        private UICellsPalette _palette;
        [SerializeField]
        private UICellsPaletteChoicePanel _paletteChoice;
        [SerializeField]
        private UIHotkeysInfoPanel_Cells _hotkeyInfoPanel;
        [SerializeField]
        private UIMainButtonsPanel _saveLoadPanel;


        public override void InstallBindings()
        {
            BindMainComponents();
            BindEnviroment();
            BindNodes();
            BindLinks();
            BindManipulators();
            BindPathfinding();
            BindSaveSystem();
            BindUI();            
        }

        private void BindMainComponents()
        {
            Container.BindInterfacesAndSelfTo<SceneController_Scene1a>().AsSingle();            

            Container.Bind(typeof(GridField), typeof(RectGridField)).To<RectGridField>().FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<CellsFieldBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsNodesCreator>().AsSingle();
        }

        private void BindEnviroment()
        {
            Container.BindInstance(_mainCamera).AsSingle();
            Container.BindInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputService>().AsSingle();
        }

        private void BindNodes()
        {
            Container.Bind(typeof(CellDataStorage), typeof(GridTypeStorage<CellData>), typeof(IObjectsStorage<CellData, Vector2Int>)).
                To<CellDataStorage>().AsSingle();
            Container.Bind(typeof(CellViewStorage), typeof(GridTypeStorage<CellView>), typeof(IObjectsStorage<CellView, Vector2Int>)).
                To<CellViewStorage>().AsSingle();

            Container.BindInterfacesAndSelfTo<CellDataFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellViewFactory>().AsSingle();
            Container.BindMemoryPool<CellData, CellDataPool>().WithInitialSize(100);
            Container.BindMemoryPool<CellView, CellViewPool>().WithInitialSize(100).
                FromComponentInNewPrefab(_cellViewPrefab).UnderTransform(_field.NodesContainer);

            Container.BindInterfacesAndSelfTo<CellView>().FromInstance(_cellViewPrefab).AsSingle();
        }

        private void BindLinks()
        {
            Container.BindMemoryPool<LinkData<Vector2Int>, LinkDataPool<Vector2Int>>().WithInitialSize(20);

            Container.BindInterfacesAndSelfTo<GridDynamicLinksProvider<CellData, LinkData<Vector2Int>>>().AsSingle();
            Container.BindInterfacesAndSelfTo<SmartLinkDataFactory<CellData, LinkData<Vector2Int>, Vector2Int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<FourSideRectGridNeighbours<CellData>>().AsSingle();
        }

        private void BindManipulators()
        {
            Container.BindInstance(_clickHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<CellTypeChanger>().AsSingle();
            Container.BindInterfacesAndSelfTo<BrushManager<CellType>>().AsSingle();
        }

        private void BindPathfinding()
        {
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<CellData, LinkData<Vector2Int>, Vector2Int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ManhattanDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellWeightGetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AverageCostProvider<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathSetter<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPathfindRunner>().AsSingle();

            Container.BindInterfacesAndSelfTo<CellsPathDrawer>().AsSingle();            
        }

        private void BindSaveSystem()
        {
            Container.BindInterfacesAndSelfTo<CellsSaveLoadManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<Saver>().AsSingle();
            Container.BindInterfacesAndSelfTo<Loader>().AsSingle();

            Container.BindInterfacesAndSelfTo<CellDataMapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsFieldSaveDtoProvider>().AsSingle();

            Container.BindInterfacesAndSelfTo<StringFileDtoGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<JsonUtilityStringSerializer>().AsSingle();

            //Choose only one
            //Container.BindInterfacesAndSelfTo<DialogueFilePathProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstantFilePathProvider>().AsSingle().WithArguments("Map_1a.json", Environment.SpecialFolder.Desktop);
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<UICellsPalette>().FromInstance(_palette).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteChoicePanel>().FromInstance(_paletteChoice).AsSingle();
            Container.BindInterfacesAndSelfTo<UIHotkeysInfoPanel_Cells>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInterfacesAndSelfTo<UIMainButtonsPanel>().FromInstance(_saveLoadPanel).AsSingle();
        }
    }
}