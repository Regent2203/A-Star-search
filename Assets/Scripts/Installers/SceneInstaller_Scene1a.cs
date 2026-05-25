using ThisProject.Links.Factories.CostProviders;
using ThisProject.Fields.Grids;
using ThisProject.Fields.Grids.Neighbours;
using ThisProject.Heuristic.Functions;
using ThisProject.Implementations.Cells;
using ThisProject.Implementations.Cells.UI;
using ThisProject.Links.Factories;
using ThisProject.Links.Providers;
using ThisProject.PathFinders;
using ThisProject.SearchAlgorithms;
using UnityEngine;
using Zenject;
using ThisProject.Inputs;
using ThisProject.Starters;
using ThisProject.ObjectsStorages;

namespace ThisProject.Installers
{
    public class SceneInstaller_Scene1a : MonoInstaller
    {
        [SerializeField]
        private InputSettings _inputSettings;
        [SerializeField]
        private CellView _cellViewPrefab;
        [SerializeField]
        private CellsGridField _field;
        [SerializeField]
        private UICellsPalette _palette;
        [SerializeField]
        private UICellsPaletteChoicePanel _paletteChoice;
        [SerializeField]
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;
        

        public override void InstallBindings()
        {
            Container.BindInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputService>().AsSingle();
            Container.BindInstance(_cellViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<GridFieldCore<CellNode>>().AsSingle();
            Container.Bind(typeof(CellsGridField), typeof(VisibleGridField<CellNode, CellView>)).FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<GridTypeStorage<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridTypeStorage<CellView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsGridFieldGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellNodeFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridFieldClickHandler<CellNode, CellView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<RuntimeLinksProvider<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinksFactory<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<FourSideGridNeighbours<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ManhattanDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellNodeWeightGetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AverageCostProvider<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<PathFinder<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPathDrawer>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsPainter>().AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPalette>().FromInstance(_palette).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteChoicePanel>().FromInstance(_paletteChoice).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteHotkeyInfoPanel>().FromInstance(_hotkeyInfoPanel).AsSingle();

            Container.BindInterfacesTo<Starter_Scene1a>().AsSingle();
        }
    }
}