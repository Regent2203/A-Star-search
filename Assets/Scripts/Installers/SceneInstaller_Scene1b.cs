using Core.Links.Factories.CostProviders;
using Core.Fields.Grids;
using Core.Fields.Grids.Neighbours;
using Core.Heuristic.Functions;
using Core.Implementations.Cells;
using Core.Implementations.Cells.UI;
using Core.Links.Factories;
using Core.Links.Providers;
using Core.PathDrawers;
using Core.PathFinders;
using Core.SearchAlgorithms;
using UnityEngine;
using Zenject;
using Core.Inputs;
using Core.Starters;
using Core.ObjectsStorages;

namespace Core.Installers
{
    public class SceneInstaller_Scene1b : MonoInstaller
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
        [SerializeField]
        private LineRenderer _pathLineRenderer;

        public override void InstallBindings()
        {
            Container.BindInstance(_inputSettings).AsSingle();
            Container.BindInterfacesAndSelfTo<UnityInputService>().AsSingle();
            Container.BindInstance(_cellViewPrefab).AsSingle();
            Container.BindInterfacesAndSelfTo<GridFieldCore<CellNode>>().AsSingle();
            Container.Bind(typeof(CellsGridField), typeof(GridField<CellNode, CellView>)).FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<GridTypeStorage<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridTypeStorage<CellView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsGridFieldGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellNodeFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridFieldClickHandler<CellNode, CellView>>().AsSingle();
            Container.BindInterfacesAndSelfTo<RuntimeLinksProvider<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<LinksFactory<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<EightSideGridNeighbours<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<AStarSearchAlgorithm<CellNode>>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellsHeuristicsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<OctileDistance>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellNodeWeightGetter>().AsSingle();
            Container.BindInterfacesAndSelfTo<AverageCostProvider<CellNode>>().AsSingle();
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