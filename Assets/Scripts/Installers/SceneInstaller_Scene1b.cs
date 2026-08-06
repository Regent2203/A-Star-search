using EasyField.Fields;
using EasyField.GridNeighbours;
using EasyField.Heuristic.Functions;
using EasyField.Implementations.Cells;
using EasyField.Implementations.Cells.UI;
using EasyField.Inputs;
using EasyField.Links;
using EasyField.Links.CostProviders;
using EasyField.Links.Factories;
using EasyField.Links.Providers;
using EasyField.ObjectsStorages;
using EasyField.PathDrawers;
using EasyField.PathFinders;
using EasyField.PathSetters;
using EasyField.SearchAlgorithms;
using EasyField.Starters;
using UnityEngine;
using Zenject;

namespace EasyField.Installers
{
    public class SceneInstaller_Scene1b : MonoInstaller
    {
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
        private UICellsHotkeysInfoPanel _hotkeyInfoPanel;
        [SerializeField]
        private LineRenderer _pathLineRenderer;

        public override void InstallBindings()
        {
            Container.BindInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellView>().FromInstance(_cellViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<GridField>().FromInstance(_field).AsSingle();
            Container.Bind(typeof(CellDataStorage), typeof(GridTypeStorage<CellData>), typeof(IObjectsStorage<CellData, Vector2Int>)).To<CellDataStorage>().AsSingle();
            Container.Bind(typeof(CellViewStorage), typeof(GridTypeStorage<CellView>), typeof(IObjectsStorage<CellView, Vector2Int>)).To<CellViewStorage>().AsSingle();
            Container.BindInstance(_clickHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<CellsFieldBuilder>().AsSingle();
            Container.BindMemoryPool<CellData, CellDataPool>().WithInitialSize(100);
            Container.BindMemoryPool<CellView, CellViewPool>().WithInitialSize(100).
                FromComponentInNewPrefab(_cellViewPrefab).UnderTransform(_field.NodesContainer);
            Container.BindInterfacesAndSelfTo<CellTypeChanger>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridDynamicLinksProvider<CellData, LinkData<Vector2Int>>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinkDataFactory<CellData, Vector2Int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<EightSideGridNeighbours<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<CellData, LinkData<Vector2Int>, Vector2Int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<OctileDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellWeightGetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AverageCostProvider<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathSetter<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<CellData, Vector2Int>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinePathDrawer>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPainter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LineRenderer>().FromInstance(_pathLineRenderer).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPalette>().FromInstance(_palette).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteChoicePanel>().FromInstance(_paletteChoice).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsHotkeysInfoPanel>().FromInstance(_hotkeyInfoPanel).AsSingle();

            Container.BindInterfacesAndSelfTo<Starter_Scene1b>().AsSingle();
        }
    }
}