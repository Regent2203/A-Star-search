using EasyField.BrushManagers;
using EasyField.Fields;
using EasyField.GridNeighbours;
using EasyField.Heuristic;
using EasyField.Heuristic.Functions;
using EasyField.Implementations.Cells;
using EasyField.Implementations.Cells.DynamicCells;
using EasyField.Implementations.Cells.UI;
using EasyField.Implementations.DynamicCells.UI;
using EasyField.Implementations.Links;
using EasyField.Inputs;
using EasyField.Links;
using EasyField.Links.CostProviders;
using EasyField.Links.Factories;
using EasyField.Links.Implementations;
using EasyField.Links.Providers;
using EasyField.Links.ViewMovers;
using EasyField.Nodes.ViewSelectors;
using EasyField.ObjectsStorages;
using EasyField.PathDrawers;
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
    public class SceneInstaller_Scene4b : MonoInstaller
    {
        [SerializeField]
        private Camera _mainCamera;
        [SerializeField]
        private InputSettings _inputSettings;
        [SerializeField]
        private CellView _cellViewPrefab;
        [SerializeField]
        private LinkView_Vector2Int _linkViewPrefab;
        [SerializeField]
        private RectGridField _field;
        [SerializeField]
        private CellsClickHandler _clickHandler;
        [SerializeField]
        private UICellsPalette _palette;
        [SerializeField]
        private UICellsPaletteChoicePanel _paletteChoice;
        [SerializeField]
        private UIHotkeysInfoPanel_DynamicCells _hotkeyInfoPanel;
        [SerializeField]
        private LineRenderer _pathLineRenderer;
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
            Container.BindInterfacesAndSelfTo<SceneController_Scene4b>().AsSingle();            

            Container.Bind(typeof(GridField), typeof(RectGridField)).To<RectGridField>().FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<DynamicCellsFieldBuilder>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsNodesCreator>().AsSingle();
            Container.BindInterfacesAndSelfTo<DynamicCellsLinksCreator>().AsSingle().WithArguments(true);
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
            Container.Bind(typeof(DictTypeStorage<LinkData<Vector2Int>, DualKey<Vector2Int>>), typeof(IObjectsStorage<LinkData<Vector2Int>, DualKey<Vector2Int>>)).
                To<DictTypeStorage<LinkData<Vector2Int>, DualKey<Vector2Int>>>().AsSingle();
            Container.Bind(typeof(DictTypeStorage<LinkView<Vector2Int>, DualKey<Vector2Int>>), typeof(IObjectsStorage<LinkView<Vector2Int>, DualKey<Vector2Int>>)).
                To<DictTypeStorage<LinkView<Vector2Int>, DualKey<Vector2Int>>>().AsSingle();

            Container.BindInterfacesAndSelfTo<SmartLinkDataFactory<CellData, LinkData<Vector2Int>, Vector2Int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkDataFactory<Vector2Int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkViewFactory<Vector2Int>>().AsSingle();
            Container.BindMemoryPool<LinkData<Vector2Int>, LinkDataPool<Vector2Int>>().WithInitialSize(20);
            Container.BindMemoryPool<LinkView<Vector2Int>, LinkViewPool<Vector2Int>>().WithInitialSize(20).
                FromComponentInNewPrefab(_linkViewPrefab).UnderTransform(_field.LinksContainer);

            Container.BindInterfacesAndSelfTo<CombinedLinksProvider<CellData, LinkData<Vector2Int>>>().AsSingle();
            Container.Bind<StoredLinksProvider<LinkData<Vector2Int>, Vector2Int>>().ToSelf().AsSingle();
            Container.Bind<GridDynamicLinksProvider<CellData, LinkData<Vector2Int>>>().ToSelf().AsSingle();            
            Container.BindInterfacesAndSelfTo<EightSideRectGridNeighbours<CellData>>().AsSingle();
        }

        private void BindManipulators()
        {
            Container.BindInstance(_clickHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<CellTypeChanger>().AsSingle();
            Container.BindInterfacesAndSelfTo<BrushManager<CellType>>().AsSingle();
            Container.BindInterfacesAndSelfTo<NodeViewSelector<CellView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkViewCoordinator<CellView, Vector2Int>>().AsSingle();
        }

        private void BindPathfinding()
        {
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<CellData, LinkData<Vector2Int>, Vector2Int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsHeuristicsProvider>().AsSingle();
            Container.Decorate<IHeuristicsProvider<CellData>>().With<ShortcutHeuristicsProvider<CellData, LinkData<Vector2Int>, Vector2Int>>();
            Container.BindInterfacesAndSelfTo<OctileDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellWeightGetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AverageCostProvider<CellData>>().AsSingle();
            Container.Decorate<ICostProvider<CellData>>().With<DiagonalCostProvider<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathSetter<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPathfindRunner>().AsSingle();

            Container.BindInterfacesAndSelfTo<LinePathDrawer<CellView>>().AsSingle();
            Container.Bind<LineRenderer>().WithId(LinePathDrawer.LineRendererId).FromInstance(_pathLineRenderer).AsSingle();
        }

        private void BindSaveSystem()
        {
            Container.BindInterfacesAndSelfTo<DynamicCellsSaveLoadManager>().AsSingle();
            Container.BindInterfacesAndSelfTo<Saver>().AsSingle();
            Container.BindInterfacesAndSelfTo<Loader>().AsSingle();

            Container.BindInterfacesAndSelfTo<CellDataMapper>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkDataMapper<Vector2Int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<DynamicCellsFieldSaveDtoProvider>().AsSingle();

            Container.BindInterfacesAndSelfTo<StringFileDtoGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<JsonUtilityStringSerializer>().AsSingle();

            //Choose only one
            //Container.BindInterfacesAndSelfTo<DialogueFilePathProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ConstantFilePathProvider>().AsSingle().WithArguments("Map_4b.json", Environment.SpecialFolder.Desktop);
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<UICellsPalette>().FromInstance(_palette).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteChoicePanel>().FromInstance(_paletteChoice).AsSingle();
            Container.BindInterfacesAndSelfTo<UIHotkeysInfoPanel_DynamicCells>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInterfacesAndSelfTo<UIMainButtonsPanel>().FromInstance(_saveLoadPanel).AsSingle();
        }
    }
}