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

namespace Core.Installers
{
    public class SceneInstaller_CellGridExample2 : MonoInstaller
    {
        [SerializeField]
        private CellsGridField _field;
        [SerializeField]
        private GridInputHandler<CellNode> _fieldInputHandler;
        [SerializeField]
        private CellView _cellViewPrefab;
        [SerializeField]
        private UICellsPalette _palette;
        [SerializeField]
        private UICellsPaletteChoicePanel _paletteChoice;
        [SerializeField]
        private UICellsPaletteHotkeyInfoPanel _hotkeyInfoPanel;
        [SerializeField]
        private LineRenderer _pathLineRenderer;
        [SerializeField]
        private KeyCode _markingKeyCode = KeyCode.LeftShift;

        public override void InstallBindings()
        {
            Container.BindInstance(_cellViewPrefab).AsSingle();

            Container.BindInterfacesAndSelfTo<CellsGridField>().FromInstance(_field).AsSingle();
            Container.BindInterfacesAndSelfTo<CellsGridGenerator>().AsSingle();
            Container.BindInterfacesAndSelfTo<GridInputHandler<CellNode>>().FromInstance(_fieldInputHandler).AsSingle();
            Container.BindInterfacesAndSelfTo<CellViewFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<CellNodeFactory>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<PathSetter<CellNode>>().AsSingle(); 
            Container.BindInterfacesAndSelfTo<UICellsPalette>().FromInstance(_palette).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteChoicePanel>().FromInstance(_paletteChoice).AsSingle();
            Container.BindInterfacesAndSelfTo<UICellsPaletteHotkeyInfoPanel>().FromInstance(_hotkeyInfoPanel).AsSingle();
            Container.BindInterfacesAndSelfTo<LineRenderer>().FromInstance(_pathLineRenderer).AsSingle();
            Container.BindInstance(_markingKeyCode).WithId("MarkingKey").AsSingle();
        }
    }
}