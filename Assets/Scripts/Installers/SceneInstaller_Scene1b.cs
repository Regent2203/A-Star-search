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
using EasyField.PathDrawers;
using EasyField.PathFinders;
using EasyField.PathSetters;
using EasyField.SceneControllers;
using EasyField.SearchAlgorithms;
using UnityEngine;
using Zenject;

namespace EasyField.Installers
{
    public class SceneInstaller_Scene1b : MonoInstaller
    {
        [SerializeField]
        private Camera _mainCamera;
        [SerializeField]
        private InputSettings _inputSettings;
        [SerializeField]
        private CellView _cellViewPrefab;
        [SerializeField]
        private GridField _field;
        [SerializeField]
        private CellsClickHandler _clickHandler;
        [SerializeField]
        private UICellsPalette _palette;
        [SerializeField]
        private UICellsPaletteChoicePanel _paletteChoice;
        [SerializeField]
        private UIHotkeysInfoPanel_Cells _hotkeyInfoPanel;
        [SerializeField]
        private LineRenderer _pathLineRenderer;

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
            Container.BindInterfacesAndSelfTo<SceneController_Scene1b>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsSaveLoadManager>().AsSingle();

            Container.BindInterfacesAndSelfTo<GridField>().FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<CellsFieldBuilder>().AsSingle();
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

            Container.BindInterfacesAndSelfTo<GridDynamicLinksProvider<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<SmartLinkDataFactory<CellData, Vector2Int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<EightSideGridNeighbours<CellData>>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<OctileDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellWeightGetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AverageCostProvider<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathSetter<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<CellData, Vector2Int>>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<LinePathDrawer<CellView>>().AsSingle();
            Container.Bind<LineRenderer>().WithId(LinePathDrawer.LineRendererId).FromInstance(_pathLineRenderer).AsSingle();
        }

        private void BindSaveSystem()
        {
        }

        private void BindUI()
        {
            Container.BindInterfacesAndSelfTo<UICellsPalette>().FromInstance(_palette).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteChoicePanel>().FromInstance(_paletteChoice).AsSingle();
            Container.BindInterfacesAndSelfTo<UIHotkeysInfoPanel_Cells>().FromInstance(_hotkeyInfoPanel).AsSingle();
        }
    }
}