using ThisProject.Fields;
using ThisProject.GridNeighbours;
using ThisProject.Heuristic.Functions;
using ThisProject.Implementations.Cells;
using ThisProject.Implementations.Cells.UI;
using ThisProject.Inputs;
using ThisProject.Links.Factories;
using ThisProject.Links.Factories.CostProviders;
using ThisProject.Links.Providers;
using ThisProject.ObjectsStorages;
using ThisProject.PathDrawers;
using ThisProject.PathFinders;
using ThisProject.PathSetters;
using ThisProject.SearchAlgorithms;
using ThisProject.Starters;
using UnityEngine;
using Zenject;

namespace ThisProject.Installers
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
            Container.BindInterfacesAndSelfTo<GridTypeStorage<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridTypeStorage<CellView>>().AsSingle();
            Container.BindInstance(_clickHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<CellsFieldBuilder>().AsSingle();
            Container.BindMemoryPool<CellData, CellDataPool>().WithInitialSize(100);
            Container.BindMemoryPool<CellView, CellViewPool>().WithInitialSize(100).FromComponentInNewPrefab(_cellViewPrefab).UnderTransform(_field.NodesContainer);
            Container.BindInterfacesAndSelfTo<CellTypeChanger>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridDynamicLinksProvider<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinksFactory<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<EightSideGridNeighbours<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<OctileDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellNodeWeightGetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AverageCostProvider<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathSetter<CellData>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<CellData>>().AsSingle();
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