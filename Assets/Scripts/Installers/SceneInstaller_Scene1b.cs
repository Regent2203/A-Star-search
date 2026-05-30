using ThisProject.Fields;
using ThisProject.Fields.ClickHandlers;
using ThisProject.Fields.GridNeighbours;
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
        private CellsField _field;
        [SerializeField]
        private CellsClickHandler _clickHandler;
        [SerializeField]
        private UICellsPalette _palette;
        [SerializeField]
        private UICellsPaletteChoicePanel _paletteChoice;
        [SerializeField]
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;
        [SerializeField]
        private LineRenderer _pathLineRenderer;

        public override void InstallBindings()
        {
            Container.BindInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputService>().AsSingle();
            Container.BindInstance(_cellViewPrefab).AsSingle();
            Container.Bind(typeof(CellsField), typeof(GridField<CellNode, CellView>)).FromInstance(_field).AsSingle();
            Container.BindInstance(_clickHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<GridTypeStorage<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridTypeStorage<CellView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsFieldGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellNodeFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellTypeChanger>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridClickHandler<CellNode, CellView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridDynamicLinksProvider<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinksFactory<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<EightSideGridNeighbours<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<OctileDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellNodeWeightGetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AverageCostProvider<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathSetter<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinePathDrawer>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPainter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LineRenderer>().FromInstance(_pathLineRenderer).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPalette>().FromInstance(_palette).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteChoicePanel>().FromInstance(_paletteChoice).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteHotkeyInfoPanel>().FromInstance(_hotkeyInfoPanel).AsSingle();

            Container.BindInterfacesTo<Starter_Scene1b>().AsSingle();
        }
    }
}